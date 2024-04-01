using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Declare Variables for this script
    private Camera m_Camera;

	// Initialize Variables
	private void Start()
	{
		this.m_Camera = Camera.main;
	}

	// Make the GameObject this script is attached to always look at the player
	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + this.m_Camera.transform.rotation * Vector3.forward);
	}

}
