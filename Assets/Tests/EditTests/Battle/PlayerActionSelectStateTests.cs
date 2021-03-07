﻿
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
            controller.PlayerAction = new PlayerActionState();
            controller.ActionData.ButtonAction = Constants.Rest;

            var result = state.Execute(controller);

            Assert.IsInstanceOf<PlayerActionState>(result);
        }

        [Test]
        public void ExecuteSetsCurrentCombatantFromQueueIfNewState() {
            var state = new PlayerActionSelectState();
            state.newState = true;
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            controller.ActionData = new ActionData();
            controller.PlayerAction = new PlayerActionState();

            var result = state.Execute(controller);

            Assert.AreEqual(controller.Prosecutors[0], controller.CurrentCombatant);
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
        }
    }
}