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
            var defenseAttorney1 = GetDefenseAttorney();
            var defenseAttorney2 = GetDefenseAttorney();
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
                DefenseAttorneys = new List<GameObject>
                {
                    defenseAttorney1,
                    defenseAttorney2
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


            Assert.AreEqual(stunnedEffect, controller.DefenseAttorneys[0].GetComponent<CharacterBattleData>().ActiveStatusEffects[0].StatusEffect);
            Assert.AreEqual(stunnedEffect, controller.DefenseAttorneys[1].GetComponent<CharacterBattleData>().ActiveStatusEffects[0].StatusEffect);
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

            Assert.AreEqual(80, controller.Prosecutors[0].GetComponent<CharacterBattleData>().CurrentStress);
            Assert.AreEqual(10, controller.Prosecutors[0].GetComponent<CharacterBattleData>().CurrentFocusPoints);

            Assert.AreEqual(80, controller.Prosecutors[1].GetComponent<CharacterBattleData>().CurrentStress);
            Assert.AreEqual(10, controller.Prosecutors[1].GetComponent<CharacterBattleData>().CurrentFocusPoints);
        }

        private GameObject GetProsecutor()
        {
            var prosecutor = new GameObject();
            prosecutor.AddComponent<CharacterBattleData>();
            prosecutor.GetComponent<CharacterBattleData>().InitializeCharacter(TestDataFactory.CreateCharacter(0, CharacterType.PlayerCharacter));
            prosecutor.GetComponent<CharacterBattleData>().StressCapacity = 100;
            prosecutor.GetComponent<CharacterBattleData>().IncreaseStress(100);
            return prosecutor;
        }

        private GameObject GetDefenseAttorney()
        {
            var defenseAttorney = new GameObject();
            defenseAttorney.AddComponent<CharacterBattleData>();
            defenseAttorney.GetComponent<CharacterBattleData>().InitializeCharacter(TestDataFactory.CreateCharacter(0, CharacterType.DefenseCharacter));
            defenseAttorney.GetComponent<CharacterBattleData>().StressCapacity = 100;
            defenseAttorney.GetComponent<CharacterBattleData>().IncreaseStress(100);
            return defenseAttorney;
        }
    }
}


