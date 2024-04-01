using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// I don't quite understand this script, if you do, a pull request is appreciated!
// https://github.com/GunnableScum/PorkyPowers-Refactored/
public class UIController : MonoBehaviour
{
    // Declare Variables for this script
    public CursorControllerScript cc;
    private bool joystickEnabled;
    public bool controlMouse;
    public bool unlockOnStart;
    public bool uiControlEnabled;
    public Selectable firstButton;
    public Selectable dummyButtonPC;
    public Selectable dummyButtonElse;
    public string buttonTag;

    private void Start()
    {
        if (this.unlockOnStart & !this.joystickEnabled) this.cc.UnlockCursor();
    }

    private void OnEnable()
    {
        this.dummyButtonPC.Select();
        this.UpdateControllerType();
    }

    private void Update()
    {
        this.UpdateControllerType();
    }

    public void SwitchMenu()
    {
        this.SelectDummy();
        this.UpdateControllerType();
    }

    private void UpdateControllerType()
    {
        if (!this.joystickEnabled & usingJoystick)
        {
            this.joystickEnabled = true;
            if (this.controlMouse)
            {
                this.cc.LockCursor();
            }
        }
        else if (this.joystickEnabled & !usingJoystick)
        {
            this.joystickEnabled = false;
            if (this.controlMouse) this.cc.UnlockCursor();
        }
        this.UIUpdate();
    }

    private void UIUpdate()
    {
        if (this.uiControlEnabled)
        {
            if (this.joystickEnabled)
            {
                if (EventSystem.current.currentSelectedGameObject.tag != this.buttonTag & this.firstButton != null)
                {
                    this.firstButton.Select();
                    this.firstButton.OnSelect(null);
                }
            }
            else this.SelectDummy();
        }
    }

    public void EnableControl()
    {
        this.uiControlEnabled = true;
    }

    public void DisableControl()
    {
        this.uiControlEnabled = false;
    }

    private void SelectDummy()
    {
        this.dummyButtonPC.Select();
    }

    public bool usingJoystick
    {
        get
        {
            return false;
        }
    }
}
