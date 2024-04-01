using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasicButtonScript : MonoBehaviour
{
    // Declare Variables for this script
    private Button button;
	public GameObject screen;

	// Initialize Values
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.OpenScreen));
	}

    // Enables the given GameObject (Screen) when the button is pressed
    private void OpenScreen()
	{
		this.screen.SetActive(true);
	}

}
