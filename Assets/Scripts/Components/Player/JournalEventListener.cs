using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class JournalEventListener : MonoBehaviour, IJournalEventListener
{
    public void ToggleJournal()
    {
        DialogueManager.StopConversation();
        GameObject.Find("JournalCanvas").GetComponent<JournalController>().ToggleExamineEvidenceJournal();
    }

    public void StartExamineConversation(string conversation) 
    {
        if(string.IsNullOrEmpty(conversation))
            DialogueManager.StartConversation("Desk/NoExamination", transform);
        else
            DialogueManager.StartConversation(conversation, transform);
    }
}
