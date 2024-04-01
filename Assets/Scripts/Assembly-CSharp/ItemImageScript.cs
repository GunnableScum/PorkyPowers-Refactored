using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageScript : MonoBehaviour
{
    // Declare Variables for this script
	public RawImage sprite;
	public GameControllerScript gs;
	[SerializeField]
	private Texture noItemSprite;
	[SerializeField]
	private Texture blankSprite;

    // Change the Image of a slot if the Item in it is changed
    private void Update()
	{
		if (this.gs != null)
		{
			Texture texture = this.gs.itemSlot[this.gs.itemSelected].texture;
			if (texture == this.blankSprite) this.sprite.texture = this.noItemSprite;
			else this.sprite.texture = texture;
		}
		else this.sprite.texture = this.noItemSprite;
	}

}
