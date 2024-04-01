using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Declare Variables for this script
    public GameObject player;
    public PlayerScript ps;
    public Transform baldi;
    public float initVelocity;
    public float velocity;
    public float gravity;
    private int lookBehind;
    public Vector3 offset;
    public float jumpHeight;
    public Vector3 jumpHeightV3;

    // Token: 0x0600092D RID: 2349 RVA: 0x00020CC4 File Offset: 0x0001F0C4
    private void Start()
	{
		this.offset = base.transform.position - this.player.transform.position; //Defines the offset
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00020D00 File Offset: 0x0001F100
	private void Update()
	{
		if (this.ps.jumpRope) //If the player is jump roping
		{
			this.velocity -= this.gravity * Time.deltaTime; //Decrease the velocity using gravity
			this.jumpHeight += this.velocity * Time.deltaTime; //Increase the jump height based on the velocity
			if (this.jumpHeight <= 0f) //When the player is on the floor, prevent the player from falling through.
			{
				this.jumpHeight = 0f;
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.velocity = this.initVelocity; //Start the jump
				}
			}
			this.jumpHeightV3 = new Vector3(0f, this.jumpHeight, 0f); //Turn the float into a vector
		}
		else if (Input.GetButton("Look Behind"))
		{
			this.lookBehind = 180; //Look behind you
		}
		else
		{
			this.lookBehind = 0; //Don't look behind you
		}
	}

	// Handle Camera Position logic after Update is called
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset;

		// Make the Camera look at baldi (+ Y Offset to the camera looks at the fact instead of the feet)
		if (this.ps.gameOver)
		{
			base.transform.position = this.baldi.transform.position + this.baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f);
			base.transform.LookAt(new Vector3(this.baldi.position.x, this.baldi.position.y + 5f, this.baldi.position.z));
			return;
		}

		// Teleport Camera to Player with offset and rotate the Camera 180° if the player is holding the "Look Behind" Button
		if (!this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset;
			base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f);
		}
		else
		{
			// Disable Lookbehind key and add jumpheight if the Player is in the Jumprope minigame
			base.transform.position = this.player.transform.position + this.offset + this.jumpHeightV3;
			base.transform.rotation = this.player.transform.rotation;
		}
	}

}
