﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Declare Variables for this script
    public GameControllerScript gc;
    public BaldiScript baldi;
    public DoorScript door;
    public PlaytimeScript playtime;
    public bool gameOver;
    public bool jumpRope;
    public bool sweeping;
    public bool hugging;
    public bool bootsActive;
    public int principalBugFixer;
    public float sweepingFailsave;
    public float fliparoo;
    public float flipaturn;
    private Quaternion playerRotation;
    public Vector3 frozenPosition;
    private bool sensitivityActive;
    private float sensitivity;
    public float mouseSensitivity;
    public float walkSpeed;
    public float runSpeed;
    public float slowSpeed;
    public float maxStamina;
    public float staminaRate;
    public float guilt;
    public float initGuilt;
    private float moveX;
    private float moveZ;
    private Vector3 moveDirection;
    private float playerSpeed;
    public float stamina;
    public CharacterController cc;
    public NavMeshAgent gottaSweep;
    public NavMeshAgent firstPrize;
    public Transform firstPrizeTransform;
    public Slider staminaBar;
    public float db;
    public string guiltType;
    public GameObject jumpRopeScreen;
    public float height;
    public Material blackSky;
    public Canvas hud;
    public Canvas mobile1;
    public Canvas mobile2;

    private void Start()
	{
		// RIP - Controller Support
		if (PlayerPrefs.GetInt("AnalogMove") == 1) this.sensitivityActive = true;
		this.height = base.transform.position.y;
		this.stamina = this.maxStamina;
		this.playerRotation = base.transform.rotation;
		this.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
		this.principalBugFixer = 1;
		this.flipaturn = 1f;
	}

	// Run multiple checks and handle Jumprope and Hugging
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, this.height, base.transform.position.z);
		this.MouseMove();
		this.PlayerMove();
		this.StaminaCheck();
		this.GuiltCheck();
		if (this.cc.velocity.magnitude > 0f) this.gc.LockMouse();
		if (this.jumpRope & (base.transform.position - this.frozenPosition).magnitude >= 1f) this.DeactivateJumpRope();

		if (this.sweepingFailsave > 0f) this.sweepingFailsave -= Time.deltaTime;
		else
		{
			this.sweeping = false;
			this.hugging = false;
		}
	}

	// Turn the Player when they move their mouse
	private void MouseMove()
	{
		this.playerRotation.eulerAngles = new Vector3(this.playerRotation.eulerAngles.x, this.playerRotation.eulerAngles.y, this.fliparoo);
		this.playerRotation.eulerAngles = this.playerRotation.eulerAngles + Vector3.up * Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.timeScale * this.flipaturn;
		base.transform.rotation = this.playerRotation;
	}

	// Handle Player Movement Input
	private void PlayerMove()
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		vector = base.transform.forward * Input.GetAxis("Forward");
		vector2 = base.transform.right * Input.GetAxis("Strafe");

		// Stamina & Running log
		if (this.stamina > 0f)
		{
			if (Input.GetButton("Run"))
			{
				this.playerSpeed = this.runSpeed;
				this.sensitivity = 1f;
				if (this.cc.velocity.magnitude > 0.1f & !this.hugging & !this.sweeping) this.ResetGuilt("running", 0.1f);
			}
			else
			{
				this.playerSpeed = this.walkSpeed;
				if (this.sensitivityActive) this.sensitivity = Mathf.Clamp((vector2 + vector).magnitude, 0f, 1f);
				else this.sensitivity = 1f;
			}
		}
		else
		{
			this.playerSpeed = this.walkSpeed;
			if (this.sensitivityActive) this.sensitivity = Mathf.Clamp((vector2 + vector).magnitude, 0f, 1f);
			else this.sensitivity = 1f;
		}

		// Sweeping, Jumprope and Boots logic
		this.playerSpeed *= Time.deltaTime;
		this.moveDirection = (vector + vector2).normalized * this.playerSpeed * this.sensitivity;
		if(!this.bootsActive)
		{
			if (this.sweeping) this.moveDirection = this.gottaSweep.velocity * Time.deltaTime + this.moveDirection * 0.3f;
			else if (this.hugging)
			{
				// I'd try making this Formula more readable but...
				// Yikes!
				this.moveDirection = (this.firstPrize.velocity * 1.2f * Time.deltaTime + (new Vector3(this.firstPrizeTransform.position.x, this.height, this.firstPrizeTransform.position.z) + new Vector3((float)Mathf.RoundToInt(this.firstPrizeTransform.forward.x), 0f, (float)Mathf.RoundToInt(this.firstPrizeTransform.forward.z)) * 3f - base.transform.position)) * (float)this.principalBugFixer;
			}

		}
		if (this.jumpRope)
		{
			this.moveDirection = new Vector3(0f, 0f, 0f);
		}
		this.cc.Move(this.moveDirection);
	}

	// Stamina Clamp Check
	private void StaminaCheck()
	{
		if (this.cc.velocity.magnitude > 0.1f)
		{
			if (Input.GetButton("Run") & this.stamina > 0f) this.stamina -= this.staminaRate * Time.deltaTime;
			if (this.stamina < 0f & this.stamina > -5f) this.stamina = -5f;
		}
		else if (this.stamina < this.maxStamina) this.stamina += this.staminaRate * Time.deltaTime;
		this.staminaBar.value = this.stamina / this.maxStamina * 100f;
	}

	// Handle Collision with Baldi and Playtime
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Baldi" & !this.gc.debugMode)
		{
			this.gameOver = true;
			RenderSettings.skybox = this.blackSky;
			base.StartCoroutine(this.KeepTheHudOff());
		}
		else if (other.transform.name == "Playtime" & !this.jumpRope & this.playtime.playCool <= 0f) this.ActivateJumpRope();
	}

	// Disable Hud while gameOver is true
	public IEnumerator KeepTheHudOff()
	{
		while (this.gameOver)
		{
			this.hud.enabled = false;
			this.mobile1.enabled = false;
			this.mobile2.enabled = false;
			this.jumpRopeScreen.SetActive(false);
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}

	// Hugging and Sweeping Check
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = true;
			this.sweepingFailsave = 1f;
		}
		else if (other.transform.name == "1st Prize" & this.firstPrize.velocity.magnitude > 5f)
		{
			this.hugging = true;
			this.sweepingFailsave = 1f;
		}
	}

	// Reset hugging and sweeping values, and handle Escaping Guilt (Principal Keys)
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Office Trigger") this.ResetGuilt("escape", this.door.lockTime);
		else if (other.transform.name == "Gotta Sweep") this.sweeping = false;
		else if (other.transform.name == "1st Prize") this.hugging = false;
	}

	// Change the guilt type and length if sufficient
	public void ResetGuilt(string type, float amount)
	{
		if (amount >= this.guilt)
		{
			this.guilt = amount;
			this.guiltType = type;
		}
	}

	// Decrease Guilt
	private void GuiltCheck()
	{
		if (this.guilt > 0f) this.guilt -= Time.deltaTime;
	}

	// Freeze the Player and Show the Jumprope
	public void ActivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(true);
		this.jumpRope = true;
		this.frozenPosition = base.transform.position;
	}

	// Hide the Jumprope
	public void DeactivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(false);
		this.jumpRope = false;
	}

	// Activate Big Ol' Boots
	public void ActivateBoots()
	{
		this.bootsActive = true;
		base.StartCoroutine(this.BootTimer());
	}

	// Disable Big Ol' Boots
	private IEnumerator BootTimer()
	{
		float time = 15f;
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		this.bootsActive = false;
		yield break;
	}
}
