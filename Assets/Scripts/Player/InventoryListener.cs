﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PixelCrushers.DialogueSystem.SequencerCommands;

public class InventoryListener : MonoBehaviour, IListener
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyEvidence(string id)
    {
        var outId = Int32.Parse(id);
        GameDataSingleton.gameData.PlayerInventory
        .AddEvidence(GameDataSingleton.gameData.allEvidence.GetEvidenceById(outId));
    }
}
