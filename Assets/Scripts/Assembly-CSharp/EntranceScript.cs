using System;
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameControllerScript gc;
	public Material map;
	public MeshRenderer wall;

    // Lower the exit under the map and show the map
    public void Lower()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f);
		if (this.gc.finaleMode)
		{
			this.wall.material = this.map;
		}
	}

	// Raise the exits to player level
	public void Raise()
	{
		base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f);
	}

}
