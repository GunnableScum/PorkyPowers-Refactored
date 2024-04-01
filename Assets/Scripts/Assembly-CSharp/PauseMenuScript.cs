using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    // Declare Variables for this script
    public GameControllerScript gc;

    // Token: 0x06000064 RID: 100 RVA: 0x000038D0 File Offset: 0x00001CD0
    private void Update()
    {
        if (this.usingJoystick & EventSystem.current.currentSelectedGameObject == null)
        {
            if (!this.gc.mouseLocked) this.gc.LockMouse();
        }
        else if (!this.usingJoystick && this.gc.mouseLocked) this.gc.UnlockMouse();
    }

    private bool usingJoystick
    {
        get
        {
            return false;
        }
    }
}
