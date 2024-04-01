using System;
using UnityEngine;

public class FacultyTriggerScript : MonoBehaviour
{
    // Declare Variables for this script
	public PlayerScript player;
	private BoxCollider hitBox;

    // Initialize Room Hitbox
    private void Start()
	{
		this.hitBox = base.GetComponent<BoxCollider>();
	}

	// Make the player guilty if they enter a faculty room
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) this.player.ResetGuilt("faculty", 1f);
	}
}
