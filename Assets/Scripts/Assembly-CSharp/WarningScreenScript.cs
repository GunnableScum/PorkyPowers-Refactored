using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreenScript : MonoBehaviour
{

	// Load Main Menu after showing the Warning screen
	private void Update()
	{
		if (Input.anyKeyDown) SceneManager.LoadScene("MainMenu");
	}
}
