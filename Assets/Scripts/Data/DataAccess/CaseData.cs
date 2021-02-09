using UnityEngine.ResourceManagement.AsyncOperations;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CaseData {
    private readonly IAddressableWrapper addressableWrapper;
    private PlayerInventory playerInventory;
    private List<Case> allCases;

    public CaseData(IAddressableWrapper addressableWrapper, PlayerInventory playerInventory) {
        this.addressableWrapper = addressableWrapper;
        this.playerInventory = playerInventory;
        allCases = new List<Case>();
    }

    public CaseData(IAddressableWrapper addressableWrapper, PlayerInventory playerInventory, List<Case> allCases) {
        this.addressableWrapper = addressableWrapper;
        this.playerInventory = playerInventory;
        this.allCases = allCases;
    }

    public void LoadCasesToInventory() {
        playerInventory.ClearCases();
        var activeCases = QuestLog.GetAllQuests();
        foreach(var c in allCases) {
            addCaseToInventory(c, activeCases);
        }
    }

    public Case GetCaseById(int id) 
    {
        return allCases.First(c => c.Id == id);
    }
    public async Task<IList<Case>> loadAllCasesFromAddressablesAsync() {
        AsyncOperationHandle<IList<Case>> caseAssets = addressableWrapper.LoadAssets<Case>(AddressablePaths.Cases,
         c => { allCases.Add(c); });
        addressableWrapper.ReleaseAssets<Case>(caseAssets);
        return await caseAssets.Task;
    }

    private void addCaseToInventory(Case c, string[] activeCases) {
        if(Array.IndexOf(activeCases, c.Name) > -1)
            playerInventory.AddActiveCase(c);
    }
}