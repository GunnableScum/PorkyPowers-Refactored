using System;
using UnityEngine;

public class AlarmClockScript : MonoBehaviour
{
    // Declare Variables for this script
    public float timeLeft;
    private float lifeSpan;
    private bool rang;
    public BaldiScript baldi;
    public AudioClip ring;
    public AudioSource audioDevice;

	// Initialize Values
    private void Start()
	{
		this.timeLeft = 30f;
		this.lifeSpan = 35f;
	}

	// Handle the logic of the Alarm Clock, or destroy it if it's lifespan is over and it has rung.
	private void Update()
	{
		if (this.timeLeft >= 0f) this.timeLeft -= Time.deltaTime;
		else if (!this.rang) this.Alarm();

		if (this.lifeSpan >= 0f) this.lifeSpan -= Time.deltaTime;
		else UnityEngine.Object.Destroy(base.gameObject, 0f);
	}

	// Make baldi target the sound of the alarm if his current sound is under priority 10 or he's not chasing the player.
	private void Alarm()
	{
		this.rang = true;
		if (this.baldi.isActiveAndEnabled) this.baldi.Hear(base.transform.position, 8f);
		this.audioDevice.clip = this.ring;
		this.audioDevice.loop = false;
		this.audioDevice.Play();
	}
}
