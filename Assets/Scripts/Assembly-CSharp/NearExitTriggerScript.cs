using System;
using UnityEngine;

public class NearExitTriggerScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameControllerScript gc;
	public EntranceScript es;

    // Make Baldi go towards the exit if the Player triggers it
    private void OnTriggerEnter(Collider other)
	{
		if (this.gc.exitsReached < 3 & this.gc.finaleMode & other.tag == "Player")
		{
			this.gc.ExitReached();
			this.es.Lower();
			if (this.gc.baldiScrpt.isActiveAndEnabled) this.gc.baldiScrpt.Hear(base.transform.position, 8f);
		}
	}

}
