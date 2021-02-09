using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using PixelCrushers.DialogueSystem;
using System.Threading.Tasks;

namespace Tests
{
    public class CaseDataTests
    {
        [Test]
        public async void loadAllCasesFromAddressablesAsyncReturnsCaseListTask()
        {
            var inventory = new PlayerInventory();
            var addressableMock = Substitute.For<IAddressableWrapper>();
            var asyncHandleResult = new AsyncOperationHandle<IList<Case>>();
            addressableMock.LoadAssets<Case>(Arg.Any<string>(), Arg.Any<Action<Case>>()).Returns(asyncHandleResult);
            
            var caseData = new CaseData(addressableMock, inventory);
            var result = await caseData.loadAllCasesFromAddressablesAsync();

            Assert.AreEqual(asyncHandleResult.Task.Result, result);
            addressableMock.Received().LoadAssets<Case>(AddressablePaths.Cases, Arg.Any<Action<Case>>());
            addressableMock.Received().ReleaseAssets<Case>(Arg.Any<AsyncOperationHandle<IList<Case>>>());
        }

        [Test]
        public void LoadCasesToInventoryOnlyLoadsCasesFromQuestLog()
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

            caseData.LoadCasesToInventory();
            QuestLog.DeleteQuest(testCase);

            Assert.AreEqual(1, inventory.ActiveCases.Count);
            Assert.AreEqual("case 1", inventory.ActiveCases[0].Name);
        }

        [Test]
        public void GetCaseByIdReturnsMatchingCase()
        {
            var inventory = new PlayerInventory();
            var addressableMock = Substitute.For<IAddressableWrapper>();
            var allCases = new List<Case> {
                TestDataFactory.CreateCase(0),
                TestDataFactory.CreateCase(1),
                TestDataFactory.CreateCase(2)
            };
            var caseData = new CaseData(addressableMock, inventory, allCases);

            var result = caseData.GetCaseById(2);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("case 2", result.Name);
        }
    }
}
