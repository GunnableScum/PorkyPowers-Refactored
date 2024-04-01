using System;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Declare Variables for this script
	public float openingDistance;
	public Transform player;
	public BaldiScript baldi;
	public MeshCollider barrier;
	public MeshCollider trigger;
	public MeshCollider invisibleBarrier;
	public MeshRenderer inside;
	public MeshRenderer outside;
	public AudioClip doorOpen;
	public AudioClip doorClose;
	public Material closed;
	public Material open;
	private bool bDoorOpen;
	private bool bDoorLocked;
	public int silentOpens;
	private float openTime;
	public float lockTime;
	private AudioSource myAudio;

    // Initialize AudioSource
    private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
	}

	// Handle Door opening and closing
	private void Update()
	{
		if (this.lockTime > 0f) this.lockTime -= 1f * Time.deltaTime;
		else if (this.bDoorLocked) this.UnlockDoor();

		if (this.openTime > 0f) this.openTime -= 1f * Time.deltaTime;

		if (this.openTime <= 0f & this.bDoorOpen)
		{
			this.barrier.enabled = true;
			this.invisibleBarrier.enabled = true;
			this.bDoorOpen = false;
			this.inside.material = this.closed;
			this.outside.material = this.closed;
            if (this.silentOpens <= 0) this.myAudio.PlayOneShot(this.doorClose, 1f);
		}

		// Door was left clicked
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider == this.trigger & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance & !this.bDoorLocked))
			{
				if (this.baldi.isActiveAndEnabled & this.silentOpens <= 0) this.baldi.Hear(base.transform.position, 1f);
				this.OpenDoor();
				if (this.silentOpens > 0) this.silentOpens--;
			}
		}
	}

	// Open the door for three seconds
	public void OpenDoor()
	{
		if (this.silentOpens <= 0 && !this.bDoorOpen) this.myAudio.PlayOneShot(this.doorOpen, 1f);
		this.barrier.enabled = false;
		this.invisibleBarrier.enabled = false;
		this.bDoorOpen = true;
		this.inside.material = this.open;
		this.outside.material = this.open;
        this.openTime = 3f;
	}

	// Open the door when an NPC slams their body into it
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked & other.CompareTag("NPC")) this.OpenDoor();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00021404 File Offset: 0x0001F804
	public void LockDoor(float time) //Lock the door for a specified amount of time
	{
		this.bDoorLocked = true;
		this.lockTime = time;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00021414 File Offset: 0x0001F814
	public void UnlockDoor()
	{
		this.bDoorLocked = false;
	}

    // DoorLocked getter
	public bool DoorLocked
	{
		get
		{
			return this.bDoorLocked;
		}
	}

	// Allow a door to be opened without a sound 4 times
	public void SilenceDoor()
	{
		this.silentOpens = 4;
	}


}
