using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class QuestLogHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnQuestEntryStateChange(QuestEntryArgs args)
    {
        if(QuestLog.GetQuestEntryState(args.questName, args.entryNumber) == QuestState.Active) {
            var entryDescription = QuestLog.GetQuestEntry(args.questName, args.entryNumber);
            DialogueLua.SetQuestField(args.questName, "StatusDescription", entryDescription);
        }
    }
}
