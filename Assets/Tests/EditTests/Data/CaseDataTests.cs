using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using PixelCrushers.DialogueSystem;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Data
{
    public class CaseDataTests
    {
        [Test]
        public async void LoadCasesToInventoryOnlyLoadsCasesFromQuestLog()
        {
            var inventory = new PlayerInventory();
            var addressableMock = new AddressableWrapper();
            var testCase = "Integration Test";
            QuestLog.AddQuest(testCase, "description", QuestState.Active);
            var caseData = new CaseData(addressableMock, inventory);

            await caseData.LoadCasesToInventory();
            QuestLog.DeleteQuest(testCase);

            Assert.AreEqual(1, inventory.ActiveCases.Count);
            Assert.AreEqual("Integration Test", inventory.ActiveCases[0].Name);
        }
    }
}
