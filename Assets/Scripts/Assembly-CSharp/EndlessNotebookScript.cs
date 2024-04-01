using System;
using UnityEngine;

public class EndlessNotebookScript : MonoBehaviour
{

    // Declare Variables for this script
	public float openingDistance;
	public GameControllerScript gc;
	public Transform player;
	public GameObject learningGame;

    // Initialize Variables
    private void Start()
	{
		this.gc = GameObject.Find("Game Controller").GetComponent<GameControllerScript>();
		this.player = GameObject.Find("Player").GetComponent<Transform>();
	}

	// Handle Leftclick on a notebook
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Notebook" & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance))
			{
				base.gameObject.SetActive(false);
				this.gc.CollectNotebook();
				this.learningGame.SetActive(true);
			}
		}
	}


}
