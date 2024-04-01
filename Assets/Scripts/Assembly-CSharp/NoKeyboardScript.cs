using System;
using UnityEngine;
using UnityEngine.UI;

public class NoKeyboardScript : InputField
{
	// Allow Touchscreen keyboard input
	protected override void Start()
	{
		base.keyboardType = (TouchScreenKeyboardType)(-1);
		base.Start();
	}
}
