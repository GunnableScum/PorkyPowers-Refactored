using System;
using UnityEngine;

public class AudioQueueScript : MonoBehaviour
{
    // Declare Variables for this script
    private AudioSource audioDevice;
	private int audioInQueue;
	private AudioClip[] audioQueue = new AudioClip[100];

	// Initialize the AudioDevice (Your speakers)
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Play Audio that is Queued if nothing is playing and the queue isn't empty
	private void Update()
	{
		if (!this.audioDevice.isPlaying && this.audioInQueue > 0) this.PlayQueue();
	}

	// Add an AudioClip to the Queue
	public void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound;
		this.audioInQueue++;
	}

	// Play an AudioClip and remove it from the Queue
	private void PlayQueue()
	{
		this.audioDevice.PlayOneShot(this.audioQueue[0]);
		this.UnqueueAudio();
	}

	// Shift every element of the Audio Queue left once and reduce the length of the queue by one
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++) this.audioQueue[i - 1] = this.audioQueue[i];
		this.audioInQueue--;
	}

	// Set the audioInQueue value to zero so the queue check fails until a new sound is added
	public void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

}
