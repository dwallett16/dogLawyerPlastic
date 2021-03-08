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

    public void StartExamineConversation() 
    {
        DialogueManager.StartConversation("Desk/ExamineEvidence", transform);
    }
}
