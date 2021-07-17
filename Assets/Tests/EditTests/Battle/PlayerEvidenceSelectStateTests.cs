using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using Assets.Scripts.Battle.States;
using UnityEngine.UI;
using System.Linq;

namespace Battle
{
    public class PlayerEvidenceSelectStateTests
    {
        [Test]
        public void ExecuteIfNewStateDisablesUnusedDefaultButtons()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = true;
            var testCase = TestDataFactory.CreateCase(0);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    EvidenceList = new List<Evidence>
                    {
                        TestDataFactory.CreateEvidence(0, testCase),
                        TestDataFactory.CreateEvidence(1, testCase)
                    }
                }
            };
            SetEvidencePanel(controller, 3);
            SetActionButtons(controller);

            evidenceSelectState.Execute(controller);

            Assert.AreEqual(2, controller.EvidenceButtons.Where(x => x.activeInHierarchy).Count());
        }

        [Test]
        public void ExecuteIfNewStateMapsToButtons()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = true;
            var testCase = TestDataFactory.CreateCase(0);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    EvidenceList = new List<Evidence>
                    {
                        TestDataFactory.CreateEvidence(0, testCase),
                        TestDataFactory.CreateEvidence(1, testCase)
                    }
                }
            };
            SetEvidencePanel(controller, 3);
            SetActionButtons(controller);

            evidenceSelectState.Execute(controller);

            Assert.NotNull(controller.EvidenceButtons.First(x => x.GetComponentInChildren<Text>().text == "evidence 0"));
            Assert.NotNull(controller.EvidenceButtons.First(x => x.GetComponentInChildren<Text>().text == "evidence 1"));
            Assert.NotNull(controller.EvidenceButtons.First(x => x.GetComponentInChildren<EvidenceButtonData>().EvidenceData.Id == 0));
            Assert.NotNull(controller.EvidenceButtons.First(x => x.GetComponentInChildren<EvidenceButtonData>().EvidenceData.Id == 1));
        }

        [Test]
        public void ExecuteIfNewStateTogglesActivePanels()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = true;
            var testCase = TestDataFactory.CreateCase(0);
            var controller = new BattleController
            {
                battleData = new BattleData
                {
                    EvidenceList = new List<Evidence>
                    {
                        TestDataFactory.CreateEvidence(0, testCase),
                        TestDataFactory.CreateEvidence(1, testCase)
                    }
                }
            };
            SetEvidencePanel(controller, 3);
            SetActionButtons(controller);

            evidenceSelectState.Execute(controller);

            Assert.True(controller.EvidencePanel.activeInHierarchy);
            Assert.False(controller.ActionButtonPanel.activeInHierarchy);
            Assert.False(controller.EvidenceConfirmPanel.activeInHierarchy);
        }

        [Test]
        public void ExecuteCancelButtonPressedReturnsPlayerActionSelectState()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = false;
            var controller = new BattleController
            {
                IsBackButtonPressed = true,
                PlayerActionSelect = new PlayerActionSelectState()
            };
            SetEvidencePanel(controller, 0);

            var result = evidenceSelectState.Execute(controller);

            Assert.IsInstanceOf<PlayerActionSelectState>(result);
        }

        [Test]
        public void ExecuteSubmitButtonPressedOnEvidenceDisplaysConfirmMenu() 
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = false;
            var controller = new BattleController
            {
                IsSubmitButtonPressed = true
            };
            SetEvidencePanel(controller, 3);

            var result = evidenceSelectState.Execute(controller);

            Assert.True(controller.EvidenceConfirmPanel.activeInHierarchy);
        }

        [Test]
        public void ExecuteEvidenceConfirmMenuDisplayedBackButtonPressedHidesConfirmMenu() 
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = false;
            var controller = new BattleController
            {
                IsBackButtonPressed = true
            };
            SetEvidencePanel(controller, 3);
            controller.EvidenceConfirmPanel.SetActive(true);

            var result = evidenceSelectState.Execute(controller);

            Assert.False(controller.EvidenceConfirmPanel.activeInHierarchy);
            Assert.IsInstanceOf<PlayerEvidenceSelectState>(result);
        }

        [Test]
        public void ExecuteEvidenceConfirmMenuDisplayedGoBackOptionSelectedHidesConfirmMenu()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = false;
            var controller = new BattleController
            {
                MenuConfirmSelection = false,
                IsSubmitButtonPressed = true
            };
            SetEvidencePanel(controller, 3);
            controller.EvidenceConfirmPanel.SetActive(true);

            var result = evidenceSelectState.Execute(controller);

            Assert.False(controller.EvidenceConfirmPanel.activeInHierarchy);
            Assert.IsInstanceOf<PlayerEvidenceSelectState>(result);
        }

        [Test]
        public void ExecuteEvidenceConfirmMenuDisplaedConfirmOptionSelectedRemovesSelectedEvidenceFromBattleData()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = false;
            var selectedEvidence = TestDataFactory.CreateEvidence(0, new Case());
            var controller = new BattleController
            {
                MenuConfirmSelection = true,
                IsSubmitButtonPressed = true,
                battleData = new BattleData
                {
                    EvidenceList = new List<Evidence>
                    {
                        selectedEvidence,
                        TestDataFactory.CreateEvidence(1, new Case())
                    }
                },
                ActionData = new ActionData
                {
                    SelectedEvidence = selectedEvidence
                },
                Action = new ActionState()
            };
            SetEvidencePanel(controller, 3);
            controller.EvidenceConfirmPanel.SetActive(true);

            var result = evidenceSelectState.Execute(controller);

            Assert.False(controller.battleData.EvidenceList.Any(x => x.Id == selectedEvidence.Id));
        }

        [Test]
        public void ExecuteEvidenceConfirmMenuDisplaedConfirmOptionSelectedReturnsActionState()
        {
            var evidenceSelectState = new PlayerEvidenceSelectState();
            evidenceSelectState.NewState = false;
            var selectedEvidence = TestDataFactory.CreateEvidence(0, new Case());
            var controller = new BattleController
            {
                MenuConfirmSelection = true,
                IsSubmitButtonPressed = true,
                battleData = new BattleData
                {
                    EvidenceList = new List<Evidence>
                    {
                        selectedEvidence,
                        TestDataFactory.CreateEvidence(1, new Case())
                    }
                },
                ActionData = new ActionData
                {
                    SelectedEvidence = selectedEvidence
                },
                Action = new ActionState()
            };
            SetEvidencePanel(controller, 3);
            controller.EvidencePanel.SetActive(true);
            controller.EvidenceConfirmPanel.SetActive(true);

            var result = evidenceSelectState.Execute(controller);

            Assert.IsInstanceOf<ActionState>(result);
            Assert.IsInstanceOf<PresentEvidenceAction>(controller.ActionData.Action);
            Assert.False(controller.EvidenceConfirmPanel.activeInHierarchy);
            Assert.False(controller.EvidencePanel.activeInHierarchy);
        }

        private void SetEvidencePanel(BattleController controller, int numButtons)
        {
            var evidencePanel = new GameObject();
            evidencePanel.SetActive(false);
            controller.EvidencePanel = evidencePanel;
            controller.EvidenceButtons = new List<GameObject>();
            for (int i = 0; i < numButtons; i++)
            {
                var button = new GameObject();
                button.AddComponent<EvidenceButtonData>();
                var textObject = new GameObject();
                textObject.AddComponent<Text>();
                textObject.transform.SetParent(button.transform);
                controller.EvidenceButtons.Add(button);
            }
            controller.EvidenceConfirmPanel = new GameObject();
            controller.EvidenceConfirmPanel.SetActive(false);
        }

        private void SetActionButtons(BattleController controller)
        {
            controller.ActionButtonPanel = new GameObject();
            controller.ActionButtonPanel.SetActive(true);
        }
    }
}
