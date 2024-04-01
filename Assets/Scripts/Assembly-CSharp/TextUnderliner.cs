using System;
using TMPro;
using UnityEngine;

public class TextUnderliner : MonoBehaviour
{
    // Declare Variables for this script
    public TMP_Text text;

    // Underlines given Text Object
    public void Underline()
    {
        this.text.fontStyle = FontStyles.Underline;
    }

    // Remove Underline from Given Text Object
    public void Ununderline()
    {
        this.text.fontStyle = FontStyles.Normal;
    }

}
