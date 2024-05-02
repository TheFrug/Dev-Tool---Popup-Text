using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [Header("Speaker Characteristics")]
    
    [Tooltip("This is the name that displays next to the text box.  If you leave this field blank, the name panel will not appear")]
    public string name;
    
    [Tooltip("This is the portrait that displays next to the text box.  If you leave this field blank, the portrait panel will not appear")]
    public Sprite portrait;

    [Header("Dialog Characteristics")]
    [TextArea(2, 8)]
    [NonReorderable]
    [Tooltip("First input the number of text blocks.  Then fill out each text box with your desired dialog")]
    public string[] textBlocks;

    [Tooltip("Input any audio you want to play as dialog progresses.  Clips will play in the order they are inputed")]
    public AudioClip[] soundClips;
    
}
