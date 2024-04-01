using System;
using UnityEngine;

public class BsodaSparyScript : MonoBehaviour
{
    // Declare Variables for this script
	public float speed;
	private float lifeSpan;
	private Rigidbody rb;


	// Initialize Variables and move Forward
    private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.rb.velocity = base.transform.forward * this.speed;
		this.lifeSpan = 30f;
	}


	// Move forward and destroy the BSODA particle once the lifespan expires
	private void Update()
	{
		this.rb.velocity = base.transform.forward * this.speed;
		this.lifeSpan -= Time.deltaTime;
		if (this.lifeSpan < 0f) UnityEngine.Object.Destroy(base.gameObject, 0f);
	}

}
