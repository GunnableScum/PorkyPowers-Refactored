using System;
using UnityEngine;
using UnityEngine.Events;

public class OnAwakeTrigger : MonoBehaviour
{
    // Declare Variables for this script
	public UnityEvent OnEnableEvent;

    // Invoke this trigger
    private void OnEnable()
	{
		this.OnEnableEvent.Invoke();
	}

}
