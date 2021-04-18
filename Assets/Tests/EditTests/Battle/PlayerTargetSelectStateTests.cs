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
            var controller = new BattleController();
            SetSkillPanel(controller, 2);
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
                }
            };
            SetDefenseAttorneys(controller);
            SetSkillPanel(controller, 2);
            controller.SkillPanel.SetActive(true);
            var state = new PlayerTargetSelectState
            {
                NewState = true
            };

            state.Execute(controller);

            Assert.AreEqual("DA 0", controller.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
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
    }
}
