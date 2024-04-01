using System;
using UnityEngine;
using UnityEngine.AI;

public class BaldiScript : MonoBehaviour
{
    // Declare Variables for this script
	public bool canSeePlayer;
	public float baseTime;
	public float speed;
	public float timeToMove;
	public float baldiAnger;
	public float baldiTempAnger;
	public float baldiWait;
	public float baldiSpeedScale;
	private float moveFrames;
	private float currentPriority;
	public bool antiHearing;
	public float antiHearingTime;
	public float vibrationDistance;
	public float angerRate;
	public float angerRateRate;
	public float angerFrequency;
	public float timeToAnger;
	public bool endless;
	public Transform player;
	public Transform wanderTarget;
	public AILocationSelectorScript wanderer;
	private AudioSource baldiAudio;
	public AudioClip slap;
	public AudioClip[] speech = new AudioClip[3];
	public Animator baldiAnimator;
	public float coolDown;
	private Vector3 previous;
	private bool rumble;
	private NavMeshAgent agent;

	// Initialize Values and Agent for this script and enable Rumble depending on Player Config
    private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.timeToMove = this.baseTime;
		this.Wander();
		if (PlayerPrefs.GetInt("Rumble") == 1) this.rumble = true;
	}

	// Handle counters and Anger
	private void Update()
	{
		if (this.timeToMove > 0f) this.timeToMove -= 1f * Time.deltaTime;
		else this.Move();

		if (this.coolDown > 0f) this.coolDown -= 1f * Time.deltaTime;

		if (this.baldiTempAnger > 0f) this.baldiTempAnger -= 0.02f * Time.deltaTime;
		else this.baldiTempAnger = 0f;

		if (this.antiHearingTime > 0f) this.antiHearingTime -= Time.deltaTime;
		else this.antiHearing = false;


		// If the player is playing endless mode, change the Anger Logic slightly (AngerRate, making it harder everytime a notebook is collected)
		if (this.endless)
		{
			if (this.timeToAnger > 0f) this.timeToAnger -= 1f * Time.deltaTime;
			else
			{
				this.timeToAnger = this.angerFrequency;
				this.GetAngry(this.angerRate);
				this.angerRate += this.angerRateRate;
			}
		}
	}

	// FixedUpdate is capped at 50 times a second, handle moveFrames if there are any, then stop Baldi from moving
	private void FixedUpdate()
	{
		if (this.moveFrames > 0f)
		{
			this.moveFrames -= 1f;
			this.agent.speed = this.speed;
		}
		else this.agent.speed = 0f;

		// Check if the player is visible from Baldis Perspective using a raycast
		Vector3 direction = this.player.position - base.transform.position; 
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			this.canSeePlayer = true;
			this.TargetPlayer();
		}
		else
		{
			this.canSeePlayer = false;
		}
	}

    // Wander to a random location (Refer to AILocationSelectorScript)
    private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Select the Player as the Agent Target and remove the currently heard priority (ignore all sounds while the player is visible)
	public void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// If Baldi reaches his destination and the player is not dead, wander to a new location
	private void Move()
	{
		if (base.transform.position == this.previous & this.coolDown < 0f) this.Wander();

		this.moveFrames = 10f;
		this.timeToMove = this.baldiWait - this.baldiTempAnger;
		this.previous = base.transform.position;
		this.baldiAudio.PlayOneShot(this.slap);
		this.baldiAnimator.SetTrigger("slap");

		if (this.rumble)
		{
			float num = Vector3.Distance(base.transform.position, this.player.position);
			if (num < this.vibrationDistance)
			{
				float motorLevel = 1f - num / this.vibrationDistance;
			}
		}
	}

	// Increase Baldis Anger and calculate his new ruler slap frequency by using a formula
	public void GetAngry(float value)
	{
		this.baldiAnger += value;
		if (this.baldiAnger < 0.5f) this.baldiAnger = 0.5f;

		this.baldiWait = -3f * this.baldiAnger / (this.baldiAnger + 2f / this.baldiSpeedScale) + 3f;
	}

	// Increase Baldi's temporary Anger
	public void GetTempAngry(float value)
	{
		this.baldiTempAnger += value;
	}

	// Make Baldi chase after a sound if he's not being jammed by anti-hearing tape and the priority is sufficient
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!this.antiHearing && priority >= this.currentPriority)
		{
			this.agent.SetDestination(soundLocation);
			this.currentPriority = priority;
		}
	}

	// Make Baldi have dementia and activate his wander phase
	public void ActivateAntiHearing(float t)
	{
		this.Wander();
		this.antiHearing = true;
		this.antiHearingTime = t;
	}

}
