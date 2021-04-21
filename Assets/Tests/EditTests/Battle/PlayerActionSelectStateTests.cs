using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using Assets.Scripts.Battle.States;

namespace Battle {
    public class PlayerActionSelectStateTests {

        [Test]
        public void ExecuteReturnsBattleState() {
            var state = new PlayerActionSelectState();
            var controller = new BattleController();
            controller.ActionData = new ActionData();

            var result = state.Execute(controller);

            Assert.NotNull(result);
        }

        [Test]
        public void ExecuteReturnsPlayerActionStateIfRestButtonPressed() {
            var state = new PlayerActionSelectState();
            var controller = new BattleController();
            controller.ActionData = new ActionData();
            controller.Action = new ActionState();
            controller.ActionData.ButtonAction = Constants.Rest;

            var result = state.Execute(controller);

            Assert.IsInstanceOf<ActionState>(result);
        }

        [Test]
        public void ExecuteTogglesPanelsIfNewState() {
            var state = new PlayerActionSelectState();
            state.NewState = true;
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            SetActionButtons(controller);
            SetSkillPanel(controller);
            SetEvidencePanel(controller);
            controller.ActionData = new ActionData();
            controller.Action = new ActionState();

            var result = state.Execute(controller);

            Assert.True(controller.ActionButtonPanel.activeInHierarchy);
            Assert.False(controller.SkillPanel.activeInHierarchy);
            Assert.False(controller.EvidencePanel.activeInHierarchy);
        }

        [Test]
        public void ExecuteClearsButtonActionIfNewState() {
            var state = new PlayerActionSelectState();
            state.NewState = true;
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            SetActionButtons(controller);
            SetSkillPanel(controller);
            SetEvidencePanel(controller);
            controller.ActionData = new ActionData {
                ButtonAction = Constants.Skills
            };
            controller.Action = new ActionState();

            var result = state.Execute(controller);

            Assert.IsEmpty(controller.ActionData.ButtonAction);
        }

        [Test]
        public void ExecuteReturnsPlayerSkillSelectStateWhenButtonActionSkill() {
            var state = new PlayerActionSelectState();
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            controller.ActionData = new ActionData {
                ButtonAction = Constants.Skills
            };
            controller.PlayerSkillSelect = new PlayerSkillSelectState();

            var result = state.Execute(controller);

            Assert.IsInstanceOf<PlayerSkillSelectState>(result);
        }

        [Test]
        public void ExecuteReturnsPlayerEvidenceSelectStateIfEvidenceButtonPressed()
        {
            var state = new PlayerActionSelectState();
            var controller = new BattleController();
            NewUp(controller);
            controller.ActionData = new ActionData();
            controller.Action = new ActionState();
            controller.ActionData.ButtonAction = Constants.Evidence;

            var result = state.Execute(controller);

            Assert.IsInstanceOf<PlayerEvidenceSelectState>(result);
        }

        private void SetActionButtons(BattleController controller) {
           controller.ActionButtonPanel = new GameObject();
           controller.ActionButtonPanel.SetActive(false);
        }

        private void SetSkillPanel(BattleController controller) {
            var skillPanel = new GameObject();
            skillPanel.SetActive(true);
            controller.SkillPanel = skillPanel;
        }

        private void SetEvidencePanel(BattleController controller)
        {
            var evidencePanel = new GameObject();
            evidencePanel.SetActive(true);
            controller.EvidencePanel = evidencePanel;
        }

        private void CreateCombatantsList(BattleController battleController)
        {
            for (int i = 0; i < 2; i++)
            {
                var prosecutor = new GameObject();
                prosecutor.AddComponent<CharacterBattleData>();
                prosecutor.GetComponent<CharacterBattleData>().type = CharacterType.PlayerCharacter;
                battleController.Prosecutors.Add(prosecutor);
            }
            for (int i = 0; i < 2; i++)
            {
                var defenseAttorney = new GameObject();
                defenseAttorney.AddComponent<CharacterBattleData>();
                defenseAttorney.GetComponent<CharacterBattleData>().type = CharacterType.DefenseCharacter;
                battleController.DefenseAttorneys.Add(defenseAttorney);
            }
        }

        private void QueueCombatantOrder(BattleController battleController, bool isPlayerCharacterNext)
        {
            if (isPlayerCharacterNext)
            {
                battleController.AllCombatants.Enqueue(battleController.Prosecutors[0]);
                battleController.AllCombatants.Enqueue(battleController.DefenseAttorneys[0]);
            }
            else
            {
                battleController.AllCombatants.Enqueue(battleController.DefenseAttorneys[0]);
                battleController.AllCombatants.Enqueue(battleController.Prosecutors[0]);
            }
            battleController.AllCombatants.Enqueue(battleController.Prosecutors[1]);
            battleController.AllCombatants.Enqueue(battleController.DefenseAttorneys[1]);
        }

        private void NewUp(BattleController battleController)
        {
            battleController.AllCombatants = new Queue<GameObject>();
            battleController.Prosecutors = new List<GameObject>();
            battleController.DefenseAttorneys = new List<GameObject>();
            battleController.PlayerActionSelect = new PlayerActionSelectState();
            battleController.Initial = new InitialState();
            battleController.EnemyActionSelect = new EnemyActionSelectState();
            battleController.PlayerEvidenceSelect = new PlayerEvidenceSelectState();
        }
    }
}
