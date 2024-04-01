using System;
using UnityEngine;

public class AILocationSelectorScript : MonoBehaviour
{
    // Declare Variables for this script
    public Transform[] newLocation = new Transform[29];
    public AmbienceScript ambience;
    private int id;

    // Select one of 28 possible Targets and play ambience.
    public void GetNewTarget()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 28f));
		base.transform.position = this.newLocation[this.id].position;
		this.ambience.PlayAudio();
	}

	// Select one of 15 possible Hallway Targets and play ambience.
	public void GetNewTargetHallway()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 15f));
		base.transform.position = this.newLocation[this.id].position;
		this.ambience.PlayAudio();
	}

	// Select one of 14 possible Hallway Targets (Range 1-15)
	public void QuarterExclusive()
	{
		this.id = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 15f));
		base.transform.position = this.newLocation[this.id].position;
	}
}
