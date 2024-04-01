﻿using System;
using UnityEngine;

public class SwingingDoorScript : MonoBehaviour
{
    // Declare Variables for this script
    public GameControllerScript gc;
    public BaldiScript baldi;
    public MeshCollider barrier;
    public GameObject obstacle;
    public MeshCollider trigger;
    public MeshRenderer inside;
    public MeshRenderer outside;
    public Material closed;
    public Material open;
    public Material locked;
    public AudioClip doorOpen;
    public AudioClip baldiDoor;
    private float openTime;
    private float lockTime;
    public bool bDoorOpen;
    public bool bDoorLocked;
    private bool requirementMet;
    private AudioSource myAudio;

    // Initialize Values
    private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
		this.bDoorLocked = true;
	}

	// Handle Door Logic
	private void Update()
	{
		if (!this.requirementMet & this.gc.notebooks >= 2)
		{
			this.requirementMet = true;
			this.UnlockDoor();
		}
		if (this.openTime > 0f) this.openTime -= 1f * Time.deltaTime;
		if (this.lockTime > 0f) this.lockTime -= Time.deltaTime;
		else if (this.bDoorLocked & this.requirementMet) this.UnlockDoor();
		if (this.openTime <= 0f & this.bDoorOpen & !this.bDoorLocked)
		{
			this.bDoorOpen = false;
			this.inside.material = this.closed;
			this.outside.material = this.closed;
		}
	}

	// Open the Door if it's not locked
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked)
		{
			this.bDoorOpen = true;
			this.inside.material = this.open;
			this.outside.material = this.open;
			this.openTime = 2f;
		}
	}

	// Notify Baldi over SMS that the Player has opened a door
	private void OnTriggerEnter(Collider other)
	{
		if (!(this.gc.notebooks < 2 & other.tag == "Player"))
		{
			if (!this.bDoorLocked)
			{
				this.myAudio.PlayOneShot(this.doorOpen, 1f);
				if (other.tag == "Player" && this.baldi.isActiveAndEnabled)
				{
					this.baldi.Hear(base.transform.position, 1f);
				}
			}
		}
	}

	// Lock the door (duh)
	public void LockDoor(float time)
	{
		this.barrier.enabled = true;
		this.obstacle.SetActive(true);
		this.bDoorLocked = true;
		this.lockTime = time;
		this.inside.material = this.locked;
		this.outside.material = this.locked;
	}

	// Unlock the door (duh²)
	private void UnlockDoor()
	{
		this.barrier.enabled = false;
		this.obstacle.SetActive(false);
		this.bDoorLocked = false;
		this.inside.material = this.closed;
		this.outside.material = this.closed;
	}
}
