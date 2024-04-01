using System;
using UnityEngine;


public class QuarterSpawnScript : MonoBehaviour
{

    // Declare Variables for this script
	public AILocationSelectorScript wanderer;
	public Transform location;

    // Initialize Values and Spawn the Quarter
    private void Start()
	{
		this.wanderer.QuarterExclusive();
		base.transform.position = this.location.position + Vector3.up * 4f;
	}
}
