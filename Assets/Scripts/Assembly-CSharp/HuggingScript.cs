using System;
using UnityEngine;
using UnityEngine.AI;

public class HuggingScript : MonoBehaviour
{
    // Declare Variables for this script
	private Rigidbody rb;
	private Vector3 otherVelocity;
	public bool beingPushed;
	private float failSave;

    // Initialize RigidBody
    private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	// Make sure the Player is being pushed for >= 1 extra frame
	private void Update()
	{
		if (this.failSave > 0f) this.failSave -= Time.deltaTime;
		else this.beingPushed = false;
	}

	// Actually Push the Player
	private void FixedUpdate()
	{
		if (this.beingPushed) this.rb.velocity = this.otherVelocity;
	}

	// Prevent the Player from leaving
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "1st Prize")
		{
			this.beingPushed = true;
			this.otherVelocity = this.rb.velocity * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			this.failSave = 1f;
		}
	}

	// Stop pushing the Player when they leave 1st Prizes Trigger
	private void OnTriggerExit()
	{
		this.beingPushed = false;
	}

}
