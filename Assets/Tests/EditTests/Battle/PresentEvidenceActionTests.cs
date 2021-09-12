using Assets.Scripts.Battle.Utilities;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using Assets.Tests.EditTests;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class PresentEvidenceActionTests: EditTestBase
    {
        [Test]
        public void ActIneffectiveEvidenceDoesNotAddJuryPoints()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    CaseData = testCase
                },
                Prosecutors = new List<GameObject>()
            };
            var actionData = new ActionData
            {
                SelectedEvidence = TestDataFactory.CreateEvidence(2, testCase),
                Target = juryObject
            };
            var utilitiesMock = Substitute.For<IActionUtilities>();
            utilitiesMock.GetBattleController().Returns(controller);
            SetActionUtilitiesMock(utilitiesMock);

            presentEvidenceAction.Act(actionData);

            Assert.AreEqual(0, juryObject.GetComponent<JuryController>().GetJuryPoints());
        }

        [Test]
        public void ActAddsJuryPointsBasedOnEffectiveness()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    CaseData = testCase
                },
                Prosecutors = new List<GameObject>()
            };
            var actionData = new ActionData
            {
                SelectedEvidence = TestDataFactory.CreateEvidence(1, testCase),
                Target = juryObject
            };
            var utilitiesMock = Substitute.For<IActionUtilities>();
            utilitiesMock.CalculateJuryPointsFromPresentedEvidence(EvidenceEffectivenessType.Relevant).Returns(25);
            utilitiesMock.GetBattleController().Returns(controller);
            SetActionUtilitiesMock(utilitiesMock);

            presentEvidenceAction.Act(actionData);

            Assert.AreEqual(25, juryObject.GetComponent<JuryController>().GetJuryPoints());
        }

        [Test]
        public void ActAddsEffectiveEvidenceCountOnController()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    CaseData = testCase
                },
                Prosecutors = new List<GameObject>(),
                EffectiveEvidenceCount = 0
            };
            var actionData = new ActionData
            {
                SelectedEvidence = TestDataFactory.CreateEvidence(0, testCase),
                Target = juryObject
            };
            var utilitiesMock = Substitute.For<IActionUtilities>();
            utilitiesMock.CalculateJuryPointsFromPresentedEvidence(EvidenceEffectivenessType.Relevant).Returns(25);
            utilitiesMock.GetBattleController().Returns(controller);
            SetActionUtilitiesMock(utilitiesMock);

            presentEvidenceAction.Act(actionData);

            Assert.AreEqual(1, controller.EffectiveEvidenceCount);
        }

        [Test]
        public void ActAddsStunnedStatusEffectToDefenseAttorneysWithThirdEffectiveEvidence()
        {
            var stunnedEffect = new StatusEffect { Name = "Stunned" };
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var prosecutor1 = GetProsecutor();
            var prosecutor2 = GetProsecutor();
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    CaseData = testCase
                },
                Prosecutors = new List<GameObject>
                {
                    prosecutor1,
                    prosecutor2
                },
                EffectiveEvidenceCount = 2,
                StunnedEffect = stunnedEffect
            };
            var actionData = new ActionData
            {
                SelectedEvidence = TestDataFactory.CreateEvidence(0, testCase),
                Target = juryObject
            };
            var utilitiesMock = Substitute.For<IActionUtilities>();
            utilitiesMock.CalculateJuryPointsFromPresentedEvidence(EvidenceEffectivenessType.Relevant).Returns(25);
            utilitiesMock.GetBattleController().Returns(controller);
            SetActionUtilitiesMock(utilitiesMock);

            presentEvidenceAction.Act(actionData);


            Assert.AreEqual("Stunned", controller.Prosecutors[0].GetComponent<CharacterBattleData>().ActiveStatusEffects[0].StatusEffect.Name);
            Assert.AreEqual("Stunned", controller.Prosecutors[1].GetComponent<CharacterBattleData>().ActiveStatusEffects[0].StatusEffect.Name);
        }

        [Test]
        public void ActAddsSpAndFpBasedOnEffectivenessToAllProsecutors()
        {
            var presentEvidenceAction = new PresentEvidenceAction();
            var testCase = TestDataFactory.CreateCase(0);
            var juryObject = new GameObject();
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().CreateJuryData(10, 5);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    CaseData = testCase
                }
            };
            var prosecutor1 = GetProsecutor();
            var prosecutor2 = GetProsecutor();
            controller.Prosecutors = new List<GameObject>
            {
                prosecutor1,
                prosecutor2
            };
            var actionData = new ActionData
            {
                SelectedEvidence = TestDataFactory.CreateEvidence(1, testCase),
                Target = juryObject
            };
            var utilitiesMock = Substitute.For<IActionUtilities>();
            utilitiesMock.CalculateSpRestorationFromPresentedEvidence(EvidenceEffectivenessType.Relevant).Returns(20);
            utilitiesMock.CalculateFpRestorationFromPresentedEvidence(EvidenceEffectivenessType.Relevant).Returns(10);
            utilitiesMock.GetBattleController().Returns(controller);
            SetActionUtilitiesMock(utilitiesMock);

            presentEvidenceAction.Act(actionData);

            Assert.AreEqual(80, controller.Prosecutors[0].GetComponent<CharacterBattleData>().currentStress);
            Assert.AreEqual(10, controller.Prosecutors[0].GetComponent<CharacterBattleData>().currentFocusPoints);

            Assert.AreEqual(80, controller.Prosecutors[1].GetComponent<CharacterBattleData>().currentStress);
            Assert.AreEqual(10, controller.Prosecutors[1].GetComponent<CharacterBattleData>().currentFocusPoints);
        }

        private GameObject GetProsecutor()
        {
            var prosecutor = new GameObject();
            prosecutor.AddComponent<CharacterBattleData>();
            prosecutor.GetComponent<CharacterBattleData>().MapFromScriptableObject(TestDataFactory.CreateCharacter(0, CharacterType.PlayerCharacter));
            prosecutor.GetComponent<CharacterBattleData>().stressCapacity = 100;
            prosecutor.GetComponent<CharacterBattleData>().IncreaseStress(100);
            return prosecutor;
        }
    }
}


