using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Data
{
    public class EvidenceDataTests
    {
        [Test]
        public async void loadAllEvidenceFromAddressablesAsyncReturnsEvidenceListTask()
        {
            var addressableMock = Substitute.For<IAddressableWrapper>();
            var asyncHandleResult = new AsyncOperationHandle<IList<Evidence>>();
            addressableMock.LoadAssets<Evidence>(Arg.Any<string>(), Arg.Any<Action<Evidence>>()).Returns(asyncHandleResult);
            
            var evidenceData = new EvidenceData(addressableMock);
            var result = await evidenceData.loadAllEvidenceFromAddressablesAsync();

            Assert.AreEqual(asyncHandleResult.Task.Result, result);
            addressableMock.Received().LoadAssets<Evidence>(AddressablePaths.Cases, Arg.Any<Action<Evidence>>());
            addressableMock.Received().ReleaseAssets<Evidence>(Arg.Any<AsyncOperationHandle<IList<Evidence>>>());
        }

        [Test]
        public void GetEvidenceByIdReturnsMatchingEvidence()
        {
            var addressableMock = Substitute.For<IAddressableWrapper>();
            var testCase = TestDataFactory.CreateCase(0);
            var allEvidence = new List<Evidence> {
                TestDataFactory.CreateEvidence(0, testCase),
                TestDataFactory.CreateEvidence(1, testCase),
                TestDataFactory.CreateEvidence(2, testCase)
            };
            var evidenceData = new EvidenceData(addressableMock, allEvidence);

            var result = evidenceData.GetEvidenceById(1);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("evidence 1", result.Name);
        }
    }
}
