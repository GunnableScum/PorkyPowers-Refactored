using System;
using System.Collections;
using UnityEngine;

public class DefaultSettingsScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameObject options;
	public Canvas canvas;

    // Enable Settings Screen and disable the Canvas
    private void Start()
	{
		if (!PlayerPrefs.HasKey("OptionsSet"))
		{
			this.options.SetActive(true);
			base.StartCoroutine(this.CloseOptions());
			this.canvas.enabled = false;
		}
	}

	// Close the Options Screen
	public IEnumerator CloseOptions()
	{
		yield return new WaitForEndOfFrame();
		this.canvas.enabled = true;
		this.options.SetActive(false);
		yield break;
	}

}
