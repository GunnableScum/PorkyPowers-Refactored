using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BackButtonScript : MonoBehaviour
{
    // Declare Variables for this script
    private Button button;
	public GameObject screen;

	// Initialize Variables
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.CloseScreen));
	}

	// Disable the given GameObject (Screen) when the button is pressed
	private void CloseScreen()
	{
		this.screen.SetActive(false);
	}

}
