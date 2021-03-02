using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using PixelCrushers.DialogueSystem;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Data 
{
    public class GameDataSingletonComponentTests: PlayTestBase
{
    [UnityTest]
    public IEnumerator AwakeCreatesGameDataSingleton()
    {
        var gameDataObject = GetGameDataObject();

        yield return new WaitForFixedUpdate();

        Assert.IsNotNull(GameDataSingletonComponent.gameData);
        GameDataSingletonComponent.gameData = null;
    }

    [UnityTest]
    public IEnumerator StartLoadsCasesFromDialogueSystemIntoInventory()
    {
        var testCaseName = "Integration Test";
        QuestLog.AddQuest(testCaseName, "this is for integration tests only", QuestState.Active);
        var gameDataObject = GetGameDataObject();

        yield return new WaitForSeconds(0.5f);
        QuestLog.DeleteQuest(testCaseName);

        Assert.AreEqual(1, GameDataSingletonComponent.gameData.PlayerInventory.ActiveCases.Count);
        Assert.AreEqual(testCaseName, GameDataSingletonComponent.gameData.PlayerInventory.ActiveCases[0].Name);
        Assert.AreEqual(999999, GameDataSingletonComponent.gameData.PlayerInventory.ActiveCases[0].Id);
        GameDataSingletonComponent.gameData = null;
    }

    [UnityTest]
    public IEnumerator StartLoadsEvidenceFromAddressables()
    {
        var gameDataObject = GetGameDataObject();

        yield return new WaitForSeconds(0.5f);

        var testEvidence = GameDataSingletonComponent.gameData.EvidenceData.GetEvidenceById(999999);
        Assert.NotNull(testEvidence);
        Assert.AreEqual("Integration Test Evidence", testEvidence.Name);
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
