using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using PixelCrushers.DialogueSystem;
using System.Threading.Tasks;

namespace Data
{
    public class CaseDataTests
    {
        [Test]
        public async void LoadCasesToInventoryOnlyLoadsCasesFromQuestLog()
        {
            var inventory = new PlayerInventory();
            var addressableMock = Substitute.For<IAddressableWrapper>();
            var allCases = new List<Case> {
                TestDataFactory.CreateCase(0),
                TestDataFactory.CreateCase(1),
                TestDataFactory.CreateCase(2)
            };
            var testCase = "case 1";
            QuestLog.AddQuest(testCase, "description", QuestState.Active);
            var caseData = new CaseData(addressableMock, inventory, allCases);

            await caseData.LoadCasesToInventory();
            QuestLog.DeleteQuest(testCase);

            Assert.AreEqual(1, inventory.ActiveCases.Count);
            Assert.AreEqual("case 1", inventory.ActiveCases[0].Name);
        }

        [Test]
        public async void GetCaseByIdReturnsMatchingCase()
        {
            var inventory = new PlayerInventory();
            var addressableMock = Substitute.For<IAddressableWrapper>();
            var allCases = new List<Case> {
                TestDataFactory.CreateCase(0),
                TestDataFactory.CreateCase(1),
                TestDataFactory.CreateCase(2)
            };
            var caseData = new CaseData(addressableMock, inventory, allCases);

            var result = await caseData.GetCaseById(2);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("case 2", result.Name);
        }
    }
}
