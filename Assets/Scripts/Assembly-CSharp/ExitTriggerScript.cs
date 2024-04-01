using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameControllerScript gc;

	// Load results screen or secret ending depending if the player answered everything wrong
    private void OnTriggerEnter(Collider other)
	{
		if (this.gc.notebooks >= 7 & other.tag == "Player")
		{
			if (this.gc.failedNotebooks >= 7) SceneManager.LoadScene("Secret");
			else SceneManager.LoadScene("Results");
		}
	}

}
