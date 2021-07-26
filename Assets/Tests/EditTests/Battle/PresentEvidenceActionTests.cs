﻿
using Assets.Scripts.Battle.Actions;
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
            var prosecutor1 = new GameObject();
            prosecutor1.AddComponent<CharacterBattleData>();
            prosecutor1.GetComponent<CharacterBattleData>().MapFromScriptableObject(TestDataFactory.CreateCharacter(0, CharacterType.PlayerCharacter));
            prosecutor1.GetComponent<CharacterBattleData>().stressCapacity = 100;
            prosecutor1.GetComponent<CharacterBattleData>().IncreaseStress(100);
            var prosecutor2 = new GameObject();
            prosecutor2.AddComponent<CharacterBattleData>();
            prosecutor2.GetComponent<CharacterBattleData>().MapFromScriptableObject(TestDataFactory.CreateCharacter(1, CharacterType.PlayerCharacter));
            prosecutor2.GetComponent<CharacterBattleData>().stressCapacity = 100;
            prosecutor2.GetComponent<CharacterBattleData>().IncreaseStress(100);
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
    }
}


