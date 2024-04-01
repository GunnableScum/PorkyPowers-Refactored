using System;
using UnityEngine;

public class CraftersTriggerScript : MonoBehaviour
{
	// Make Arts & Crafters go to goTarget when they're touched
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") this.crafters.GiveLocation(this.goTarget.position, false);
	}

	// Make Arts & Crafters disappear when the player stops touching them
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") this.crafters.GiveLocation(this.fleeTarget.position, true);
	}

	public Transform goTarget;
	public Transform fleeTarget;
	public CraftersScript crafters;
}
