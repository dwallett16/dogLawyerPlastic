using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System;
using System.Linq;

public class CaseData {
    private AsyncOperationHandle<IList<Case>> caseAssets;
    private List<Case> allCases;

    public CaseData() {
        allCases = new List<Case>();
        loadAllCases();
    }

    public void LoadCasesToInventory() {
        GameDataSingleton.gameData.PlayerInventory.ClearCases();
        var activeCases = QuestLog.GetAllQuests();
        caseAssets = Addressables.LoadAssetsAsync<Case>(AddressablePaths.Cases, c => {
                addCaseToInventory(c, activeCases);
            });
        caseAssets.Completed += c => Addressables.Release(caseAssets);
    }

    public Case GetCaseById(int id) 
    {
        return allCases.First(c => c.Id == id);
    }


    private void loadAllCases() {
        caseAssets = Addressables.LoadAssetsAsync<Case>(AddressablePaths.Cases, c => {
                allCases.Add(c);
            });
        caseAssets.Completed += c => Addressables.Release(caseAssets);
    }
    private void addCaseToInventory(Case c, string[] activeCases) {
        if(Array.IndexOf(activeCases, c.Name) > -1)
            GameDataSingleton.gameData.PlayerInventory.AddActiveCase(c);
    }
}