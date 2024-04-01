using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Hoo boy, here comes the big boy script
public class GameControllerScript : MonoBehaviour
{

    // Declare Variables for this script
	public CursorControllerScript cursorController;
	public PlayerScript player;
	public Transform playerTransform;
	public Transform cameraTransform;
	public Camera camera;
	private int cullingMask;
	public EntranceScript entrance_0;
	public EntranceScript entrance_1;
	public EntranceScript entrance_2;
	public EntranceScript entrance_3;
	public GameObject baldiTutor;
	public GameObject baldi;
	public BaldiScript baldiScrpt;
	public AudioClip aud_Prize;
	public AudioClip aud_PrizeMobile;
	public AudioClip aud_AllNotebooks;
	public GameObject principal;
	public GameObject crafters;
	public GameObject playtime;
	public PlaytimeScript playtimeScript;
	public GameObject gottaSweep;
	public GameObject bully;
	public GameObject firstPrize;
	public GameObject TestEnemy;
	public FirstPrizeScript firstPrizeScript;
	public GameObject quarter;
	public AudioSource tutorBaldi;
	public RectTransform boots;
	public string mode;
	public int notebooks;
	public GameObject[] notebookPickups;
	public int failedNotebooks;
	public bool spoopMode;
	public bool finaleMode;
	public bool debugMode;
	public bool mouseLocked;
	public int exitsReached;
	public int itemSelected;
	public int[] item = new int[3];
	public RawImage[] itemSlot = new RawImage[3];
	private string[] itemNames = new string[]
	{
		"Nothing",
		"Energy flavored Zesty Bar",
		"Yellow Door Lock",
		"Principal's Keys",
		"BSODA",
		"Quarter",
		"Baldi Anti Hearing and Disorienting Tape",
		"Alarm Clock",
		"WD-NoSquee (Door Type)",
		"Safety Scissors",
		"Big Ol' Boots"
	};
	public TMP_Text itemText;
	public UnityEngine.Object[] items = new UnityEngine.Object[10];
	public Texture[] itemTextures = new Texture[10];
	public GameObject bsodaSpray;
	public GameObject alarmClock;
	public TMP_Text notebookCount;
	public GameObject pauseMenu;
	public GameObject highScoreText;
	public GameObject warning;
	public GameObject reticle;
	public RectTransform itemSelect;
	private int[] itemSelectOffset;
	private bool gamePaused;
	private bool learningActive;
	private float gameOverDelay;
	private AudioSource audioDevice;
	public AudioClip aud_Soda;
	public AudioClip aud_Spray;
	public AudioClip aud_buzz;
	public AudioClip aud_Hang;
	public AudioClip aud_MachineQuiet;
	public AudioClip aud_MachineStart;
	public AudioClip aud_MachineRev;
	public AudioClip aud_MachineLoop;
	public AudioClip aud_Switch;
	public AudioSource schoolMusic;
	public AudioSource learnMusic;


    // Constructor Function that uses offset for the Hotbar
    public GameControllerScript()
	{
		int[] array = new int[3];
		array[0] = -80;
		array[1] = -40;
		this.itemSelectOffset = array;
	}

	// Initizialize Variables, Change Baldi's Anger system if playing endless, and reset notebooks and selected Item
	private void Start()
	{
		this.cullingMask = this.camera.cullingMask;
		this.audioDevice = base.GetComponent<AudioSource>();
		this.mode = PlayerPrefs.GetString("CurrentMode");
		if (this.mode == "endless") this.baldiScrpt.endless = true;
		this.schoolMusic.Play();
		this.LockMouse();
		this.UpdateNotebookCount();
		this.itemSelected = 0;
		this.gameOverDelay = 0.5f;
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00021B5C File Offset: 0x0001FF5C
	private void Update()
	{
		if (!this.learningActive)
		{
			// Pause Logic
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused) this.PauseGame();
				else this.UnpauseGame();
			}

			if(this.gamePaused)
			{
				if (Input.GetKeyDown(KeyCode.Y)) this.ExitGame();
				else if (Input.GetKeyDown(KeyCode.N)) this.UnpauseGame();
			} else if (Time.timeScale != 1f) Time.timeScale = 1f;

			// Item & Hotbar Logic
			if (Time.timeScale != 0f)
			{
				if (Input.GetMouseButtonDown(1)) this.UseItem();

				if (Input.GetAxis("Mouse ScrollWheel") > 0f) this.DecreaseItemSelection();
				else if (Input.GetAxis("Mouse ScrollWheel") < 0f) this.IncreaseItemSelection();

				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					this.itemSelected = 0;
					this.UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					this.itemSelected = 1;
					this.UpdateItemSelection();
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					this.itemSelected = 2;
					this.UpdateItemSelection();
				}
			}
		}
		else if (Time.timeScale != 0f) Time.timeScale = 0f;

		// Stamina Logic (You need rest!)
		if (this.player.stamina < 0f & !this.warning.activeSelf) this.warning.SetActive(true);
		else if (this.player.stamina > 0f & this.warning.activeSelf) this.warning.SetActive(false);

		// Gameover Logic
		if (this.player.gameOver)
		{
			if (this.mode == "endless" && this.notebooks > PlayerPrefs.GetInt("HighBooks") && !this.highScoreText.activeSelf)
			{
				this.highScoreText.SetActive(true);
			}
			Time.timeScale = 0f;
			this.gameOverDelay -= Time.unscaledDeltaTime * 0.5f;
			this.camera.farClipPlane = this.gameOverDelay * 400f; //Set camera farClip (Black screen creep up)
			this.audioDevice.PlayOneShot(this.aud_buzz);
			if (this.gameOverDelay <= 0f)
			{
				if (this.mode == "endless")
				{
					if (this.notebooks > PlayerPrefs.GetInt("HighBooks"))
					{
						PlayerPrefs.SetInt("HighBooks", this.notebooks);
					}
					PlayerPrefs.SetInt("CurrentBooks", this.notebooks);
				}
				Time.timeScale = 1f;
				SceneManager.LoadScene("GameOver");
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying && this.exitsReached == 3)
		{
			this.audioDevice.clip = this.aud_MachineLoop;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
	}

	// Update Notebook Text
	private void UpdateNotebookCount()
	{
		if (this.mode == "story") this.notebookCount.text = this.notebooks.ToString() + "/7 Notebooks";
		else this.notebookCount.text = this.notebooks.ToString() + " Notebooks";

		if (this.notebooks == 7 & this.mode == "story") this.ActivateFinaleMode();
	}

	// Increase Notebook Count by 1
	public void CollectNotebook()
	{
		this.notebooks++;
		this.UpdateNotebookCount();
	}

	// Prevent cursor from moving
	public void LockMouse()
	{
		if (!this.learningActive)
		{
			this.cursorController.LockCursor();
			this.mouseLocked = true;
			this.reticle.SetActive(true);
		}
	}

	// Allow the mouse to move freely
	public void UnlockMouse()
	{
		this.cursorController.UnlockCursor();
		this.mouseLocked = false;
		this.reticle.SetActive(false);
	}

	// Pause the game and show the pause screen
	public void PauseGame()
	{
		if (!this.learningActive)
		{
			this.UnlockMouse();
			Time.timeScale = 0f;
			this.gamePaused = true;
			this.pauseMenu.SetActive(true);
		}
	}

	// Return to main menu
	public void ExitGame()
	{
		SceneManager.LoadScene("MainMenu");
	}

	// Hide the pause screen and unfreeze time
	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
		this.pauseMenu.SetActive(false);
		this.LockMouse();
	}

	// Toggled after the player gets 2 notebooks, Basically the main game will start
	public void ActivateSpoopMode()
	{
		this.spoopMode = true;
		this.entrance_0.Lower();
		this.entrance_1.Lower();
		this.entrance_2.Lower();
		this.entrance_3.Lower();
		this.baldiTutor.SetActive(false);
		this.baldi.SetActive(true);
        this.principal.SetActive(true);
        this.crafters.SetActive(true);
        this.playtime.SetActive(true);
        this.gottaSweep.SetActive(true);
        this.bully.SetActive(true);
        this.firstPrize.SetActive(true);
		this.audioDevice.PlayOneShot(this.aud_Hang);
		this.learnMusic.Stop();
		this.schoolMusic.Stop();
	}

	// Raise all exits and tell the game it's finale time
	private void ActivateFinaleMode()
	{
		this.finaleMode = true;
		this.entrance_0.Raise();
		this.entrance_1.Raise();
		this.entrance_2.Raise();
		this.entrance_3.Raise();
	}

	// Increase Baldi's anger (and starts spoop mode if it's not already enabled)
	public void GetAngry(float value)
	{
		if (!this.spoopMode) this.ActivateSpoopMode();
		this.baldiScrpt.GetAngry(value);
	}

	// Show the "You can think Pad"
	public void ActivateLearningGame()
	{
		this.learningActive = true;
		this.UnlockMouse();
		this.tutorBaldi.Stop();
		if (!this.spoopMode)
		{
			this.schoolMusic.Stop();
			this.learnMusic.Play();
		}
	}

	// Hide the "You can think pad", restore Player Stamina and (optionally) reward player with a quarter
	public void DeactivateLearningGame(GameObject subject)
	{
		this.camera.cullingMask = this.cullingMask;
		this.learningActive = false;
		UnityEngine.Object.Destroy(subject);
		this.LockMouse();

		if (this.player.stamina < 100f) this.player.stamina = 100f;

		if (!this.spoopMode)
		{
			this.schoolMusic.Play();
			this.learnMusic.Stop();
		}

		if (this.notebooks == 1 & !this.spoopMode)
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == 7 & this.mode == "story") this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
	}

	// Scrolling up
	private void IncreaseItemSelection()
	{
		this.itemSelected++;
		if (this.itemSelected > 2)
		{
			this.itemSelected = 0;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

    // Scrolling down
    private void DecreaseItemSelection()
	{
		this.itemSelected--;
		if (this.itemSelected < 0)
		{
			this.itemSelected = 2;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Handle keyboard press
	private void UpdateItemSelection()
	{
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f);
		this.UpdateItemName();
	}

	// Insert item by ID into next available slot or overwrite selected item if full
	public void CollectItem(int item_ID)
	{
		for (int i = 0; i < this.item.Length; i++)
		{
			if (this.item[i] == 0)
			{
				this.item[i] = item_ID;
				this.itemSlot[i].texture = this.itemTextures[item_ID];
                this.UpdateItemName();
                return;
			}
		}
		this.item[this.itemSelected] = item_ID;
		this.itemSlot[this.itemSelected].texture = this.itemTextures[item_ID];
		this.UpdateItemName();
	}

	// Use an Item
	private void UseItem()
	{
		if (this.item[this.itemSelected] == 0) return;

		// Some Items do a raycast check
        Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
        RaycastHit raycastHit;

		switch (this.item[this.itemSelected])
		{
			case 1: // Zesty Bar
                this.player.stamina = this.player.maxStamina * 2f;
                this.ResetItem();
                break;
			case 2: // Yellow Door Lock
                if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
                {
                    raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f);
                    this.ResetItem();
                }
                break;
			case 3: // Principal keys
                if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
                {
                    DoorScript component = raycastHit.collider.gameObject.GetComponent<DoorScript>();
                    if (component.DoorLocked)
                    {
                        component.UnlockDoor();
                        component.OpenDoor();
                        this.ResetItem();
                    }
                }
                break;
			case 4: // BSODA (lol bsod reference)
                UnityEngine.Object.Instantiate<GameObject>(this.bsodaSpray, this.playerTransform.position, this.cameraTransform.rotation);
                this.ResetItem();
                this.player.ResetGuilt("drink", 1f);
                this.audioDevice.PlayOneShot(this.aud_Soda);
				break;
			case 5: // Quarter
				if (Physics.Raycast(ray, out raycastHit))
				{
					if (raycastHit.collider.name == "BSODAMachine" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f)
					{
						this.ResetItem();
						this.CollectItem(4);
					}
					else if (raycastHit.collider.name == "ZestyMachine" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f)
					{
						this.ResetItem();
						this.CollectItem(1);
					}
					else if (raycastHit.collider.name == "PayPhone" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f)
					{
                        raycastHit.collider.gameObject.GetComponent<TapePlayerScript>().Play();
						this.ResetItem();
					}
				}
				break;
			case 6: // Anti-Hearing Tape
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.name == "TapePlayer" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
				{
                    raycastHit.collider.gameObject.GetComponent<TapePlayerScript>().Play();
					this.ResetItem();
				}
				break;
			case 7: // Alarm Clock
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.alarmClock, this.playerTransform.position, this.cameraTransform.rotation);
				gameObject.GetComponent<AlarmClockScript>().baldi = this.baldiScrpt;
				this.ResetItem();
				break;
			case 8: // WD-NoSquee
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
				{
                    raycastHit.collider.gameObject.GetComponent<DoorScript>().SilenceDoor();
					this.ResetItem();
					this.audioDevice.PlayOneShot(this.aud_Spray);
				}
				break;
			case 9: // Safety Scissors
				if (this.player.jumpRope)
				{
					this.player.DeactivateJumpRope();
					this.playtimeScript.Disappoint();
					this.ResetItem();
				}
				else if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.name == "1st Prize")
				{
					this.firstPrizeScript.GoCrazy();
					this.ResetItem();
				}
				break;
			case 10: // Big Ol' Boots
				this.player.ActivateBoots();
				base.StartCoroutine(this.BootAnimation());
				this.ResetItem();
				break;

        }
	}

	// Animation for the Big Ol' Boots
	private IEnumerator BootAnimation()
	{
		float time = 15f;
		float height = 375f;
		Vector3 position = default(Vector3);
		this.boots.gameObject.SetActive(true);
		while (height > -375f)
		{
			height -= 375f * Time.deltaTime;
			time -= Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = -375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		this.boots.gameObject.SetActive(true);
		while (height < 375f)
		{
			height += 375f * Time.deltaTime;
			position = this.boots.localPosition;
			position.y = height;
			this.boots.localPosition = position;
			yield return null;
		}
		position = this.boots.localPosition;
		position.y = 375f;
		this.boots.localPosition = position;
		this.boots.gameObject.SetActive(false);
		yield break;
	}

	// Remove selected Item
	private void ResetItem()
	{
		this.item[this.itemSelected] = 0;
		this.itemSlot[this.itemSelected].texture = this.itemTextures[0];
		this.UpdateItemName();
	}

	// Remove Given Item
	public void LoseItem(int id)
	{
		this.item[id] = 0;
		this.itemSlot[id].texture = this.itemTextures[0];
		this.UpdateItemName();
	}

	// Change the UI Text to the new Item Name
	private void UpdateItemName()
	{
		this.itemText.text = this.itemNames[this.item[this.itemSelected]];
	}

	// Make the lights red and start playing a washine Machine sound (Yes, I think that is a washing machine)
	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red;
			this.audioDevice.PlayOneShot(this.aud_Switch, 0.8f);
			this.audioDevice.clip = this.aud_MachineQuiet;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 2)
		{
			this.audioDevice.volume = 0.8f;
			this.audioDevice.clip = this.aud_MachineStart;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 3)
		{
			this.audioDevice.clip = this.aud_MachineRev;
			this.audioDevice.loop = false;
			this.audioDevice.Play();
		}
	}

	// Deactivate Arts & Crafters
	public void DespawnCrafters()
	{
		this.crafters.SetActive(false);
	}

    // Easter egg (Enter 53045009 as the answer)
    public void Fliparoo()
	{
		this.player.height = 6f;
		this.player.fliparoo = 180f;
		this.player.flipaturn = -1f;
		Camera.main.GetComponent<CameraScript>().offset = new Vector3(0f, -1f, 0f);
	}

}
