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
    public class PlayerTargetSelectStateTests
    {
        [Test]
        public void ExecuteIfNewStateDisablesSkillPanel()
        {
            var controller = new BattleController
            {
                ActionData = new ActionData
                {
                    SelectedSkill = TestDataFactory.CreateSkill(0)
                },
                TargetSelector = new GameObject()
            };
            SetSkillPanel(controller, 2);
            SetDefenseAttorneys(controller);
            controller.SkillPanel.SetActive(true);
            var state = new PlayerTargetSelectState
            {
                NewState = true
            };

            state.Execute(controller);

            Assert.False(controller.SkillPanel.activeInHierarchy);
        }

        [Test]
        public void ExecuteNewStateSkillTargetTypeDefenseAttorneysSetsTargetToFirstDefenseAttorney()
        {
            var controller = new BattleController
            {
                ActionData = new ActionData
                {
                    SelectedSkill = TestDataFactory.CreateSkill(0)
                },
                TargetSelector = new GameObject()
            };
            SetDefenseAttorneys(controller);
            SetSkillPanel(controller, 2);
            controller.SkillPanel.SetActive(true);
            var state = new PlayerTargetSelectState
            {
                NewState = true
            };

            state.Execute(controller);

            Assert.AreEqual(controller.DefenseAttorneys, controller.TargetList);
            Assert.AreEqual("DA 0", controller.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
        }

        [Test]
        public void ExecuteNotNewStateRightLeftInputsUpdatesTargetIndex()
        {
            var controller = new BattleController
            {
                TargetSelector = new GameObject(),
                HorizontalAxis = 1
            };
            SetDefenseAttorneys(controller);
            controller.TargetList = controller.DefenseAttorneys;
            controller.ActionData = new ActionData
            {
                SelectedSkill = TestDataFactory.CreateSkill(0),
                Target = controller.DefenseAttorneys[0]
            };
            SetSkillPanel(controller, 2);
            controller.TargetSelector.SetActive(true);
            var state = new PlayerTargetSelectState();

            state.Execute(controller);

            Assert.AreEqual("DA 1", controller.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
        }

        [Test]
        public void ExecuteNotNewStateRightLeftInputsUpdatesTargetIndexToEndOfListWhenAtStartOfList()
        {
            var controller = new BattleController
            {
                TargetSelector = new GameObject(),
                HorizontalAxis = -1
            };
            SetDefenseAttorneys(controller);
            controller.TargetList = controller.DefenseAttorneys;
            controller.ActionData = new ActionData
            {
                SelectedSkill = TestDataFactory.CreateSkill(0),
                Target = controller.DefenseAttorneys[0]
            };
            SetSkillPanel(controller, 2);
            controller.TargetSelector.SetActive(true);
            var state = new PlayerTargetSelectState();

            state.Execute(controller);

            Assert.AreEqual("DA 1", controller.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
        }

        [Test]
        public void ExecuteNewStateSkillTargetTypeProsecutorsSetsTargetToFirstProsecutor()
        {
            var controller = new BattleController
            {
                ActionData = new ActionData
                {
                    SelectedSkill = TestDataFactory.CreateSkill(0, SkillTarget.Prosecutors)
                },
                TargetSelector = new GameObject()
            };
            SetProsecutors(controller);
            SetSkillPanel(controller, 2);
            controller.SkillPanel.SetActive(true);
            var state = new PlayerTargetSelectState
            {
                NewState = true
            };

            state.Execute(controller);

            Assert.AreEqual(controller.Prosecutors, controller.TargetList);
            Assert.AreEqual("Prosecutor 0", controller.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
        }

        [Test]
        public void ExecuteNotNewStateCancelButtonPressedReturnsPlayerSkillSelectState()
        {
            var state = new PlayerTargetSelectState();
            var controller = new BattleController
            {
                IsBackButtonPressed = true,
                PlayerSkillSelect = new PlayerSkillSelectState()
            };

            var result = state.Execute(controller);

            Assert.IsInstanceOf<PlayerSkillSelectState>(result);
        }

        private void SetSkillPanel(BattleController controller, int numButtons)
        {
            var skillPanel = new GameObject();
            skillPanel.SetActive(false);
            controller.SkillPanel = skillPanel;
            controller.SkillButtons = new List<GameObject>();
            for (int i = 0; i < numButtons; i++)
            {
                var button = new GameObject();
                button.AddComponent<SkillButtonData>();
                var textObject = new GameObject();
                textObject.AddComponent<Text>();
                textObject.transform.SetParent(button.transform);
                controller.SkillButtons.Add(button);
            }
        }

        private void SetDefenseAttorneys(BattleController controller)
        {
            controller.DefenseAttorneys = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                var defenseAttorney = new GameObject();
                defenseAttorney.AddComponent<CharacterBattleData>();
                defenseAttorney.GetComponent<CharacterBattleData>().type = CharacterType.DefenseCharacter;
                defenseAttorney.GetComponent<CharacterBattleData>().displayName = "DA " + i;
                controller.DefenseAttorneys.Add(defenseAttorney);
            }
        }

        private void SetProsecutors(BattleController controller)
        {
            controller.Prosecutors = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                var prosecutor = new GameObject();
                prosecutor.AddComponent<CharacterBattleData>();
                prosecutor.GetComponent<CharacterBattleData>().type = CharacterType.PlayerCharacter;
                prosecutor.GetComponent<CharacterBattleData>().displayName = "Prosecutor " + i;
                controller.Prosecutors.Add(prosecutor);
            }
        }
    }
}
