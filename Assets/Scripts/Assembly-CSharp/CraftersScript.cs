using System;
using UnityEngine;
using UnityEngine.AI;

public class CraftersScript : MonoBehaviour
{
    // Declare Variables for this script
	public bool db;
	public bool angry;
	public bool gettingAngry;
	public float anger;
	private float forceShowTime;
	public Transform player;
	public CharacterController cc;
	public Transform playerCamera;
	public Transform baldi;
	public NavMeshAgent baldiAgent;
	public GameObject sprite;
	public GameControllerScript gc;
	[SerializeField]
	private NavMeshAgent agent;
	public Renderer craftersRenderer;
	public SpriteRenderer spriteImage;
	public Sprite angrySprite;
	private AudioSource audioDevice;
	public AudioClip aud_Intro;
	public AudioClip aud_Loop;

    // Declare Variables and Disable the Sprite by default
    private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.sprite.SetActive(false);
	}

	// Handle Arts & Crafters Logic
	private void Update()
	{
		if (this.forceShowTime > 0f) this.forceShowTime -= Time.deltaTime;

		// Chase after the Player
		if (this.gettingAngry)
		{
			this.anger += Time.deltaTime;
			if (this.anger >= 1f & !this.angry)
			{
				this.angry = true;
				this.audioDevice.PlayOneShot(this.aud_Intro);
				this.spriteImage.sprite = this.angrySprite;
			}
		}
		else if (this.anger > 0f) this.anger -= Time.deltaTime;

		if (!this.angry)
		{
			// If the Player is close and forceShowTime is larger than 0
			if (((base.transform.position - this.agent.destination).magnitude <= 20f & (base.transform.position - this.player.position).magnitude >= 60f) || this.forceShowTime > 0f)
			{
				this.sprite.SetActive(true);
			}
			else this.sprite.SetActive(false);
		}
		else
		{
			this.agent.speed = this.agent.speed + 60f * Time.deltaTime;
			this.TargetPlayer();
			if (!this.audioDevice.isPlaying) this.audioDevice.PlayOneShot(this.aud_Loop);
		}
	}

	// Make Arts & Crafters mad at the player if they have over seven notebooks, they can see the player and are active
	private void FixedUpdate()
	{
		if (this.gc.notebooks < 7) return;
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & this.craftersRenderer.isVisible & this.sprite.activeSelf)
		{
			this.gettingAngry = true;
		}
		else this.gettingAngry = false;
	}

	// Makes Arts & Crafters show in 3 seconds if flee is true
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!this.angry && this.agent.isActiveAndEnabled)
		{
			this.agent.SetDestination(location);
			if (flee) this.forceShowTime = 3f;
		}
	}

	// Make the Pathfinding go after the Player
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
	}

	// Teleport the Player and Baldi to Starting area if Arts & Crafters touches them and is angry, also making them look at Baldi
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & this.angry)
		{
			this.cc.enabled = false;
			this.player.position = new Vector3(5f, this.player.position.y, 80f);
			this.baldiAgent.Warp(new Vector3(5f, this.baldi.position.y, 125f));
			this.player.LookAt(new Vector3(this.baldi.position.x, this.player.position.y, this.baldi.position.z));
			this.cc.enabled = true;
			this.gc.DespawnCrafters();
		}
	}
}
