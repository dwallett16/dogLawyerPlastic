using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Components
{
    public class InventoryListenerTests: PlayTestBase
{
    [UnityTest]
    public IEnumerator AddEvidenceAddsEvidenceToInventory()
    {
        var inventoryListener = new GameObject("inventoryListener", typeof(InventoryListener));
        AddToCleanup(inventoryListener);
        var gameDataObject = GetGameDataObject();

        yield return new WaitForSeconds(0.5f);

        var listener = inventoryListener.GetComponent<InventoryListener>();
        listener.AddEvidence("999999");

        yield return new WaitForSeconds(0.2f);

        Assert.NotNull(GameDataSingletonComponent.gameData.PlayerInventory.EvidenceList[0]);
        Assert.AreEqual(999999, GameDataSingletonComponent.gameData.PlayerInventory.EvidenceList[0].Id);
        GameDataSingletonComponent.gameData = null;
    }

    [UnityTest]
    public IEnumerator RemoveEvidenceRemovesEvidenceFromInventory()
    {
        var inventoryListener = new GameObject("inventoryListener", typeof(InventoryListener));
        AddToCleanup(inventoryListener);
        var gameDataObject = GetGameDataObject();

        yield return new WaitForSeconds(0.5f);
        GameDataSingletonComponent.gameData.PlayerInventory.AddEvidence(CreateEvidence(1, CreateCase(0)));
        var listener = inventoryListener.GetComponent<InventoryListener>();

        listener.RemoveEvidence("1");

        yield return new WaitForSeconds(0.2f);

        Assert.AreEqual(0, GameDataSingletonComponent.gameData.PlayerInventory.EvidenceList.Count);
        GameDataSingletonComponent.gameData = null;
    }

    private GameObject GetGameDataObject() {
        var gameDataObject = new GameObject("GameData");
        AddToCleanup(gameDataObject);
        gameDataObject.AddComponent<GameDataSingletonComponent>();
        return gameDataObject;
    }
}
}
