using System;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    // Declare Variables for this script
	public GameObject simpleControls;
	public GameObject proControls;
	private bool active;


    // Hide MobileControls by default
    private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Show Mobile Controls if the player is using Touch
	private void Update()
	{
		if (InputTypeManager.usingTouch)
		{
			if (!this.active) this.ActivateMobileControls();
		}
		else if (this.active) this.DeactivateMobileControls();
	}

	// Show Mobile Controls
	private void ActivateMobileControls()
	{
		this.simpleControls.SetActive(true);
		this.active = true;
	}

	// Hide Mobile Controls
	private void DeactivateMobileControls()
	{
		this.proControls.SetActive(false);
		this.simpleControls.SetActive(false);
		this.active = false;
	}

}
