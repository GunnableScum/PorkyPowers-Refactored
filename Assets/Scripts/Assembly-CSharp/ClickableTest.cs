using System;
using UnityEngine;

public class ClickableTest : MonoBehaviour
{

	// Disable a Notebook if it was clicked
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && raycastHit.transform.name == "MathNotebook")
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
