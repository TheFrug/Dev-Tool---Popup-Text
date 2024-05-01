using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : MonoBehaviour
{
    public Dialog dialog;
    private DialogManager dManager;

    private void Awake()
    {
        dManager = FindObjectOfType<DialogManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && (!dManager.inConversation))
        {
            TriggerDialog();
        }
        else if (Input.GetKeyDown(KeyCode.K) && (dManager.inConversation))
        {
            dManager.NextTextBlock();
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

}
