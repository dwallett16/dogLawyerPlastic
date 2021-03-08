using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PixelCrushers.DialogueSystem.SequencerCommands;

public class InventoryListener : MonoBehaviour, IInventoryListener
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void NotifyEvidence(string id)
    {
        var outId = Int32.Parse(id);
        var evidence = await GameDataSingletonComponent.gameData.EvidenceData.GetEvidenceById(outId);
        GameDataSingletonComponent.gameData.PlayerInventory
        .AddEvidence(evidence);
    }
}
