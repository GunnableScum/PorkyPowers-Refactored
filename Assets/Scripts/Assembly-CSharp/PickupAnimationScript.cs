using System;
using UnityEngine;

public class PickupAnimationScript : MonoBehaviour
{
    // Declare Variables for this script
	private Transform itemPosition;


    // Initialize Values
    private void Start()
	{
		this.itemPosition = base.GetComponent<Transform>();
	}

	// Change position based on time (Sinewave)
	private void Update()
	{
		this.itemPosition.localPosition = new Vector3(0f, Mathf.Sin((float)Time.frameCount * 0.017453292f) / 2f + 1f, 0f);
	}
}
