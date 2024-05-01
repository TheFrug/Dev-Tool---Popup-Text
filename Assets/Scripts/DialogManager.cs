using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI displayText;
    public GameObject TextBox;
    public GameObject Speaker;

    private Queue<string> textBlocks;

    public bool inConversation = false;

    void Start()
    {
        textBlocks = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        TextBox.SetActive(true);
        inConversation = true;
        nameText.text = dialog.name;

        textBlocks.Clear();

        foreach (string text in dialog.textBlocks)
        {
            textBlocks.Enqueue(text);
        }
        NextTextBlock();
    }

    public void NextTextBlock()
    {
        if(textBlocks.Count == 0)
        {
            EndDialog();
            return;
        }

        string text = textBlocks.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText (string text)
    {
        displayText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            displayText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        Debug.Log("End of Conversation");
        inConversation = false;
        TextBox.SetActive(false);
    }

}
