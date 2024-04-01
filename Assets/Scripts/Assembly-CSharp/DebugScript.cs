using System;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Declare Variables for this script
	public bool limitFramerate;
	public int framerate;

    // Limit Framerate
    private void Start()
	{
		if (this.limitFramerate)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = this.framerate;
		}
	}

}
