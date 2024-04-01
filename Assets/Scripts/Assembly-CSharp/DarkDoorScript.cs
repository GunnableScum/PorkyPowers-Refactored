using System;
using UnityEngine;

public class DarkDoorScript : MonoBehaviour
{
    // Declare Variables for this script
	public SwingingDoorScript door;
	public Material lightDoo0;
	public Material lightDoo60;
	public Material lightDooLock;
	public MeshRenderer mesh;

	// Change the door Material depending on it's situation
    private void Update()
	{
		if (this.door.bDoorLocked) this.mesh.material = this.lightDooLock;
		if (this.door.bDoorOpen) this.mesh.material = this.lightDoo60;
		else this.mesh.material = this.lightDoo0;
	}

}
