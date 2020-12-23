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

    public void PopulateCases() {
        caseAssets = Addressables.LoadAssetsAsync<Case>(AddressablePaths.Cases, c => {
                addCaseToInventory(c);
            });
        caseAssets.Completed += c => Addressables.Release(caseAssets);
    }

    private void loadAllCases() {
        caseAssets = Addressables.LoadAssetsAsync<Case>(AddressablePaths.Cases, c => {
                allCases.Add(c);
            });
        caseAssets.Completed += c => Addressables.Release(caseAssets);
    }

    private void addCaseToInventory(Case c) {
        var activeCases = QuestLog.GetAllQuests();
        if(Array.IndexOf(activeCases, c.Name) > -1)
            GameDataSingleton.gameData.PlayerInventory.AddActiveCase(c);
    }

    public Case GetCaseById(int id) 
    {
        return allCases.First(c => c.Id == id);
    }

}