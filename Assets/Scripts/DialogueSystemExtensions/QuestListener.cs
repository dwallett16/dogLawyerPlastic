using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class QuestListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnQuestStateChange(string questName) 
    {
        GameDataSingleton.gameData.caseData.PopulateCases();
        checkForActiveCase();
    }

    private void checkForActiveCase()
    {
        DialogueLua.SetVariable("HasActiveCase", QuestLog.GetAllQuests().Length > 0);
    }
}
