using System;
using UnityEngine;
using UnityEngine.AI;

public class BsodaEffectScript : MonoBehaviour
{
    // Declare Variables for this script
	private NavMeshAgent agent;
	private Vector3 otherVelocity;
	private bool beingPushed;
	private float failSave;

    // Initialize Variables
    private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
	}

	// Push an Agent if they are touching something that pushes them (Bsoda or Gotta Sweep)
	private void Update()
	{
		if (this.beingPushed) this.agent.velocity = this.otherVelocity;

		if (this.failSave > 0f) this.failSave -= Time.deltaTime;
		else this.beingPushed = false;
	}

	// Change the velocity of an agent to push them
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "BSODA")
		{
			this.beingPushed = true;
			this.otherVelocity = other.GetComponent<Rigidbody>().velocity;
			this.failSave = 1f;
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			this.beingPushed = true;
			this.otherVelocity = base.transform.forward * this.agent.speed * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			this.failSave = 1f;
		}
	}

	// Stop pushing the agent
	private void OnTriggerExit()
	{
		this.beingPushed = false;
	}

}
