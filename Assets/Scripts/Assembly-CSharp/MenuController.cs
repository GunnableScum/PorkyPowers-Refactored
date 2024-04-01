using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Declare Variables for this script
	public UIController uc;
	public Selectable firstButton;
	public Selectable dummyButtonPC;
	public Selectable dummyButtonElse;
	public GameObject back;


    public void OnEnable()
	{
		this.uc.firstButton = this.firstButton;
		this.uc.dummyButtonPC = this.dummyButtonPC;
		this.uc.dummyButtonElse = this.dummyButtonElse;
		this.uc.SwitchMenu();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel") && this.back != null)
		{
			this.back.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

}
