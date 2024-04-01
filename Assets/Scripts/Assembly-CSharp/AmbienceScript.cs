using System;
using UnityEngine;

public class AmbienceScript : MonoBehaviour
{

    // Declare Variables for this script
	public Transform aiLocation;
	public AudioClip[] sounds;
	public AudioSource audioDevice;

	// Has a 2% chance to play an ambience sound that is selected randomly from a list of AudioClips
    public void PlayAudio()
	{
		int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 49f)); 
		if (!this.audioDevice.isPlaying & num == 0)
		{
			base.transform.position = this.aiLocation.position;
			int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, (float)(this.sounds.Length - 1)));
			this.audioDevice.PlayOneShot(this.sounds[num2]);
		}
	}
}
