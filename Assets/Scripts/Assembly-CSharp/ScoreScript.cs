using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameObject scoreText;
	public TMP_Text text;

    // Handle Score for endless mode
    private void Start()
	{
		if (PlayerPrefs.GetString("CurrentMode") == "endless")
		{
			this.scoreText.SetActive(true);
			this.text.text = "Score:\n" + PlayerPrefs.GetInt("CurrentBooks") + " Notebooks";
		}
	}

}
