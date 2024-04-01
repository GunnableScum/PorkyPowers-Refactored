using System;
using UnityEngine;

public class InputTypeManager : MonoBehaviour
{
    // Declare Variables for this script
	private static InputTypeManager itm;
	public static bool usingTouch;

    // Called before any Start function
    private void Awake()
	{
		Input.simulateMouseWithTouches = false;
		if (InputTypeManager.itm == null)
		{
			InputTypeManager.itm = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else if (InputTypeManager.itm != this) UnityEngine.Object.Destroy(base.gameObject);
	}

	// Change InputType if a touchscreen was used
	private void Update()
	{
		if (Input.touchCount > 0 && !InputTypeManager.usingTouch) InputTypeManager.usingTouch = true;
		else if (Input.anyKeyDown) InputTypeManager.usingTouch = false;
	}

}
