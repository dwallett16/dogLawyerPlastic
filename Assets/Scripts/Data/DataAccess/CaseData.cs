using UnityEngine.ResourceManagement.AsyncOperations;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class CaseData {
    private readonly IAddressableWrapper addressableWrapper;
    private PlayerInventory playerInventory;

    public CaseData(IAddressableWrapper addressableWrapper, PlayerInventory playerInventory) {
        this.addressableWrapper = addressableWrapper;
        this.playerInventory = playerInventory;
    }

    //Testing only
    public CaseData(IAddressableWrapper addressableWrapper, PlayerInventory playerInventory, List<Case> allCases) {
        this.addressableWrapper = addressableWrapper;
        this.playerInventory = playerInventory;
    }

    public async Task<IList<Case>> LoadCasesToInventory() {
        playerInventory.ClearCases();
        var activeCases = QuestLog.GetAllQuests();
        AsyncOperationHandle<IList<Case>> caseAssets = addressableWrapper.LoadAssets<Case>(AddressablePaths.Cases,
         c => { addCaseToInventory(c, activeCases); });
        addressableWrapper.ReleaseAssets<Case>(caseAssets);
        return await caseAssets.Task;
    }
    public async Task<Case> GetCaseById(int id) 
    {
        Case targetCase = null;
        AsyncOperationHandle<IList<Case>> caseAssets = addressableWrapper.LoadAssets<Case>(AddressablePaths.Cases,
         c => { if(c.Id == id) targetCase = c; });
        addressableWrapper.ReleaseAssets<Case>(caseAssets);
        await caseAssets.Task;
        return targetCase;
    }

    private void addCaseToInventory(Case c, string[] activeCases) {
        if(Array.IndexOf(activeCases, c.Name) > -1)
            playerInventory.AddActiveCase(c);
    }
}