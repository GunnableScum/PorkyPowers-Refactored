using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class NeedMoreScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameControllerScript gc;
	public AudioSource audioDevice;
	public AudioClip baldiDoor;

    // "You need to collect 2 Notebooks before you can use these Doors!"
    private void OnTriggerEnter(Collider other)
	{
		if (this.gc.notebooks < 2 & other.tag == "Player") this.audioDevice.PlayOneShot(this.baldiDoor, 1f);
	}

}
