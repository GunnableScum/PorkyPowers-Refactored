using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Declare Variables for this script
	public StartButton.Mode currentMode;

    // Set Mode and load school scene
    public void StartGame()
	{
		if (this.currentMode == StartButton.Mode.Story) PlayerPrefs.SetString("CurrentMode", "story");
		else PlayerPrefs.SetString("CurrentMode", "endless");
		SceneManager.LoadSceneAsync("School");
	}

	// Enum for Mode possibilities
	public enum Mode
	{
		Story,
		Endless
	}
}
