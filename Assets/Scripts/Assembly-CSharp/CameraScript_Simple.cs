using System;
using UnityEngine;

public class CameraScript_Simple : MonoBehaviour
{
    // Declare Variables for this script
	public GameObject player;
	private int lookBehind;
	private Vector3 offset;

	// Add an offset to the Camera
    private void Start()
	{
		this.offset = base.transform.position - this.player.transform.position;
	}

	// Update the Camera position after each Update call
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset;
		base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f);
	}

}
