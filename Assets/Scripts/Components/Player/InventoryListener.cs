using System;
using UnityEngine;

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

    public async void AddEvidence(string id)
    {
        var outId = Int32.Parse(id);
        var evidence = await GameDataSingletonComponent.gameData.EvidenceData.GetEvidenceById(outId);
        GameDataSingletonComponent.gameData.PlayerInventory
        .AddEvidence(evidence);
    }

    public void RemoveEvidence(string id)
    {
        var outId = Int32.Parse(id);
        GameDataSingletonComponent.gameData.PlayerInventory.RemoveEvidence(outId);
    }
}
