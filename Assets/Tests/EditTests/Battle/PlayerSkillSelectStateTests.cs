using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using Assets.Scripts.Battle.States;
using UnityEngine.UI;
using System.Linq;

namespace Battle {
    public class PlayerSkillSelectStateTests {

        [Test]
        public void ExecuteIfNewStateDisablesUnusedDefaultButtons() {
            var skillSelectState = new PlayerSkillSelectState();
            skillSelectState.NewState = true;
            var controller = new BattleController();
            controller.TargetSelector = new GameObject();
            SetSkillPanel(controller, 4);
            SetActionButtons(controller);
            controller.ActionData = new ActionData {
                CurrentCombatantBattleData = new CharacterBattleData {
                    skills = new List<Skill> {
                        TestDataFactory.CreateSkill(0),
                        TestDataFactory.CreateSkill(1)
                    }
                }
            };

            skillSelectState.Execute(controller);

            Assert.AreEqual(2, controller.SkillButtons.Where(x => x.activeInHierarchy).Count());
        }

        [Test]
        public void ExecuteIfNewStateMapsToSkillButtons() {
            var skillSelectState = new PlayerSkillSelectState();
            skillSelectState.NewState = true;
            var controller = new BattleController();
            controller.TargetSelector = new GameObject();
            SetSkillPanel(controller, 4);
            SetActionButtons(controller);
            controller.ActionData = new ActionData {
                CurrentCombatantBattleData = new CharacterBattleData {
                    skills = new List<Skill> {
                        TestDataFactory.CreateSkill(0),
                        TestDataFactory.CreateSkill(1)
                    }
                }
            };

            skillSelectState.Execute(controller);

            Assert.NotNull(controller.SkillButtons.First(x => x.GetComponentInChildren<Text>().text == "Skill 0"));
            Assert.NotNull(controller.SkillButtons.First(x => x.GetComponentInChildren<Text>().text == "Skill 1"));
            Assert.NotNull(controller.SkillButtons.First(x => x.GetComponentInChildren<SkillButtonData>().SkillData.Id == 0));
            Assert.NotNull(controller.SkillButtons.First(x => x.GetComponentInChildren<SkillButtonData>().SkillData.Id == 1));
        }

        [Test]
        public void ExecuteIfNewStateTogglesPanels() {
            var skillSelectState = new PlayerSkillSelectState();
            skillSelectState.NewState = true;
            var controller = new BattleController();
            controller.TargetSelector = new GameObject();
            SetSkillPanel(controller, 0);
            SetActionButtons(controller);

            skillSelectState.Execute(controller);
            
            Assert.True(controller.SkillPanel.activeInHierarchy);
            Assert.False(controller.ActionButtonPanel.activeInHierarchy);
            Assert.False(controller.TargetSelector.activeInHierarchy);
        }

        [Test]
        public void ExecuteNotNewStateCancelButtonPressedReturnsPlayerActionSelectState() {
            var skillSelectState = new PlayerSkillSelectState();
            skillSelectState.NewState = false;
            var controller = new BattleController {
                IsBackButtonPressed = true,
                PlayerActionSelect = new PlayerActionSelectState()
            };
            controller.TargetSelector = new GameObject();

            var result = skillSelectState.Execute(controller);
            
            Assert.IsInstanceOf<PlayerActionSelectState>(result);
        }

        [Test]
        public void ExecuteNotNewStateSubmitButtonPressedReturnsTargetSelectEnemyState()
        {
            var skillSelectState = new PlayerSkillSelectState();
            skillSelectState.NewState = false;
            var controller = new BattleController
            {
                IsSubmitButtonPressed = true,
                PlayerActionSelect = new PlayerActionSelectState(),
                PlayerTargetSelect = new PlayerTargetSelectState()
            };
            controller.TargetSelector = new GameObject();

            var result = skillSelectState.Execute(controller);

            Assert.IsInstanceOf<PlayerTargetSelectState>(result);
        }

        private void SetSkillPanel(BattleController controller, int numButtons) {
            var skillPanel = new GameObject();
            skillPanel.SetActive(false);
            controller.SkillPanel = skillPanel;
            controller.SkillButtons = new List<GameObject>();
            for(int i = 0; i < numButtons; i++) {
                var button = new GameObject();
                button.AddComponent<SkillButtonData>();
                var textObject = new GameObject();
                textObject.AddComponent<Text>();
                textObject.transform.SetParent(button.transform);
                controller.SkillButtons.Add(button);
            }
        }

        private void SetActionButtons(BattleController controller) {
           controller.ActionButtonPanel = new GameObject();
           controller.ActionButtonPanel.SetActive(true);
        }
    }
}
