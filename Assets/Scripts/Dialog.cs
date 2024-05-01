using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    public Sprite portrait;
    [TextArea(1,10)]
    [NonReorderable]
    public string[] textBlocks;
    
}
