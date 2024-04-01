using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class EndlessTextScript : MonoBehaviour
{
    // Declare Variables for this script
    public TMP_Text text;

	// Change the text to the highscore
	private void Start()
	{
		this.text.text = string.Concat(new object[]
		{
			this.text.text,
			"\nHigh Score: ",
			PlayerPrefs.GetInt("HighBooks"),
			" Notebooks"
		});
	}
	

}
