﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PixelCrushers.DialogueSystem.SequencerCommands;

public class InventoryListener : MonoBehaviour, IListener
{
    private EvidenceData evidenceData;
    // Start is called before the first frame update
    void Start()
    {
        evidenceData = GameDataSingletonComponent.gameData.EvidenceData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyEvidence(string id)
    {
        var outId = Int32.Parse(id);
        GameDataSingletonComponent.gameData.PlayerInventory
        .AddEvidence(evidenceData.GetEvidenceById(outId));
    }
}
