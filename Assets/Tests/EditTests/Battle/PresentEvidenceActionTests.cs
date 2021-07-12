
using NUnit.Framework;
using UnityEngine;

namespace Battle
{
    public class PresentEvidenceActionTests
    {
        [Test]
        public void ActIneffectiveEvidenceDoesNotAddJuryPoints()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var actionData = new ActionData
            {
                CurrentCase = testCase,
                SelectedEvidence = TestDataFactory.CreateEvidence(2, testCase),
                Jury = juryObject
            };

            presentEvidenceAction.Act(actionData);

            Assert.AreEqual(0, juryObject.GetComponent<JuryController>().GetJuryPoints());
        }

        [Test]
        public void ActRelevantEvidenceAddsModerateJuryPoints()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var actionData = new ActionData
            {
                CurrentCase = testCase,
                SelectedEvidence = TestDataFactory.CreateEvidence(1, testCase),
                Jury = juryObject
            };

            presentEvidenceAction.Act(actionData);

            Assert.IsTrue(juryObject.GetComponent<JuryController>().GetJuryPoints() > 0);
        }

        [Test]
        public void ActEffectiveEvidenceAddsManyJuryPoints()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var actionData = new ActionData
            {
                CurrentCase = testCase,
                SelectedEvidence = TestDataFactory.CreateEvidence(0, testCase),
                Jury = juryObject
            };

            presentEvidenceAction.Act(actionData);

            Assert.IsTrue(juryObject.GetComponent<JuryController>().GetJuryPoints() > 10);
        }
    }
}


