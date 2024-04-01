using System;
using UnityEngine;

public class YouWonScript : MonoBehaviour
{
    // Declare Variables for this script
	private float delay;

    // Initialize Delay
    private void Start()
	{
		this.delay = 10f;
	}

	// Close the game after a delay of 10 seconds
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay <= 0f) Application.Quit();
	}
}
