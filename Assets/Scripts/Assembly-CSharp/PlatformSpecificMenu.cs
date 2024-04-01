using System;
using UnityEngine;

public class PlatformSpecificMenu : MonoBehaviour
{
    // Declare Variables for this script
	public GameObject pC;
	public GameObject mobile;

    // Change this Menu dependant on the platform
    private void Start()
	{
		this.pC.SetActive(true);
	}

}
