using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{

	// Declare Variables for this script
    public bool db;
    public Transform player, wanderTarget;
    public AILocationSelectorScript wanderer;
    public float coolDown;
    private NavMeshAgent agent;

	// Define the Agent and start wandering
    private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.Wander();
	}

	// Handle Cooldown for this agent
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
        // Check if the Pathfinding agent can see the player, if yes, target them
        if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			this.db = true;
			this.TargetPlayer();
		}
		else
		{
			// Otherwise, Wander until player is found
			this.db = false;
			if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
			{
				this.Wander();
			}
		}
	}

	// Get a new target from AILocationSelectorScript and wander towards it
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
	}

	// Set agent Target to the player
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
	}
}
