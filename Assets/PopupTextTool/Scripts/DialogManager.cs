using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Insert the UI panel that parents the main text of your dialog")]
    public GameObject bodyTextPanel;
    [Tooltip("Insert the UI panel that parents the name of the speaker")]
    public GameObject speakerNamePanel;
    [Tooltip("Insert the UI panel that parents the portrait of the speaker")]
    public GameObject speakerPortraitPanel;

    [Header("UI Objects")]
    [Tooltip("Insert the text element that renders the main text you want in the dialog box")]
    public TextMeshProUGUI bodyText;
    [Tooltip("Insert the text element that renders the speaker's name in the name box")]
    public TextMeshProUGUI speakerNameText;
    [Tooltip("Insert the image element that renders the sprite for the speaker's portrait")]
    public Image speakerPortrait;


    private Dialog dialogTemp;
    private AudioSource audioSource;
    [HideInInspector] public AudioClip clip;
    [HideInInspector] public string text;
    [HideInInspector] public bool inConversation = false;

    private Queue<string> textBlocksToShow; //This stores the text blocks that are loaded and have yet to be shown
    private Queue<AudioClip> soundClipsToPlay; //This stores the audio clips that are loaded and have yet to be shown

    void Start()
    {
        //If the object has an audio source, get reference to it.  If not, create a generic one.
        if (gameObject.GetComponent<AudioSource>() == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else if (gameObject.GetComponent<AudioSource>() != null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
        textBlocksToShow = new Queue<string>();
        soundClipsToPlay = new Queue<AudioClip>();
    }

    public void StartDialog(Dialog dialog)
    {
        dialogTemp = dialog;

        bodyTextPanel.SetActive(true); //Makes body text panel appear

        //Makes name panel appear if there is one.  
        if (dialog.name != "")
        {
            speakerNamePanel.SetActive(true);
            speakerNameText.text = dialog.name;
        }
        //Makes portrait panel appear if there is one.  
        if (dialog.portrait != null)
        {
            speakerPortraitPanel.SetActive(true);
            speakerPortrait.sprite = dialog.portrait;
        }
        inConversation = true;

        //These two lines clear any remaining audio/text clips
        textBlocksToShow.Clear();
        soundClipsToPlay.Clear();

        //Fills these two queues with text and sounds from the arrays of text and sounds stored in the dialog component of the speaker
        foreach (string text in dialog.textBlocks)
        {
            if(dialog.textBlocks != null) 
            {
                textBlocksToShow.Enqueue(text);
            }
        }

        foreach (AudioClip sound in dialog.soundClips)
        {
            if (dialog.soundClips != null)
            {
                soundClipsToPlay.Enqueue(sound);
            }
        }
        NextTextBlock();
    }

    public void NextTextBlock()
    {

        //If there is no text in the queue, run EndDialog
        if (textBlocksToShow.Count == 0)
        {
            EndDialog();
            return;
        }
        //Moves the text queue down
        if (textBlocksToShow.Count != 0)
        {
            text = textBlocksToShow.Dequeue(); //Moves down queue of dialog
            StopAllCoroutines();
            StartCoroutine(TypeText(text));
        }
        //Moves the sound queue down and then plays that sound clip
        if (soundClipsToPlay != null)
        {
            if (soundClipsToPlay.Count != 0)
            {
                clip = soundClipsToPlay.Dequeue();
            }
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }

    //Animates text one character at a time for smoother presentation
    IEnumerator TypeText(string text)
    {
        bodyText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            bodyText.text += letter;
            yield return null;
        }
    }

    //Closes relative panels
    public void EndDialog()
    {
        //Resets all values that were stored for the interaction
        inConversation = false;
        dialogTemp = null;
        clip = null;

        //Sets each panel inactive
        bodyTextPanel.SetActive(false);
        speakerNamePanel.SetActive(false);
        speakerPortraitPanel.SetActive(false);
    }
}
