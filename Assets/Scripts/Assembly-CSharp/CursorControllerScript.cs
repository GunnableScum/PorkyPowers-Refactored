using System;
using UnityEngine;

public class CursorControllerScript : MonoBehaviour
{

	// Lock the Cursor and make it invisible
	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Allow the Cursor to move and make it visible
	public void UnlockCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
