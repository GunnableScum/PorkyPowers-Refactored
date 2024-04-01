using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseSliderScript : MonoBehaviour
{
    // Declare Variables for this script
    public Slider slider;

	// Set the Slider's value
	private void Start()
	{
		if (PlayerPrefs.GetFloat("MouseSensitivity") < 100f) PlayerPrefs.SetFloat("MouseSensitivity", 200f);
		slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	// Set the Mouse Sensitivity
	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
	}

}
