using System;
using UnityEngine;


public class Script : MonoBehaviour
{
    // Declare Variables for this script
	public AudioSource audioDevice;
	private bool played;

    // Close the Game when audio is finished
    private void Update()
	{
		if (!this.audioDevice.isPlaying & this.played) Application.Quit();
	}

	// Play sound when trigger is entered
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" & !this.played)
		{
			this.audioDevice.Play();
			this.played = true;
		}
	}

}
