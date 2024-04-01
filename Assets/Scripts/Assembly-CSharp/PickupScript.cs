using System;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Declare Variables for this script
	public GameControllerScript gc;
	public Transform player;

    // Collect an Item and disable it
    private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if (!raycastHit.transform.name.StartsWith("Pickup_") || Vector3.Distance(this.player.position, base.transform.position) > 10f) return;
                raycastHit.transform.gameObject.SetActive(false);
				this.gc.CollectItem(this.GetID(raycastHit.transform.name.Replace("Pickup_", "")));
			}
		}
	}

    // Extracted function to remove if-else if chain
    private int GetID(string name)
    {
        switch (name)
        {
            case "EnergyFlavoredZestyBar": return 1;
            case "YellowDoorLock": return 2;
            case "Key": return 3;
            case "BSODA": return 4;
            case "Quarter": return 5;
            case "Tape": return 6;
            case "AlarmClock": return 7;
            case "WD-3D": return 8;
            case "SafetyScissors": return 9;
            case "BigBoots": return 10;
            default:
                throw new ArgumentException(name + ": Item Not implemented!");
        }
    }

}
