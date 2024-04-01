using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Fire Different events when the player hovers over something
public class MouseoverScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
    // Declare Variables for this script
	public UnityEvent mouseOver;
	public UnityEvent mouseLeave;

    public void OnSelect(BaseEventData eventData)
	{
		this.mouseOver.Invoke();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver.Invoke();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		this.mouseLeave.Invoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseLeave.Invoke();
	}

}
