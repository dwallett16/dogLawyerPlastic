﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Battle.States;
using NUnit.Framework;
using UnityEngine;

namespace Battle
{
    public class NextTurnStateTests
    {
        [Test]
        public void ExecuteReturnsPlayerActionSelectStateIfPlayerCharacterIsNextInTurnOrder()
        {
            var nextTurnState = new NextTurnState();
            var battleController = new BattleController();

            NewUp(battleController);

            CreateCombatantsList(battleController);
            QueueCombatantOrder(battleController, true);

            var result = nextTurnState.Execute(battleController);

            Assert.IsInstanceOf<PlayerActionSelectState>(result);
        }

        [Test]
        public void ExecuteReturnsEnemyActionSelectStateIfEnemyIsNextInTurnOrder()
        {
            var nextTurnState = new NextTurnState();
            var battleController = new BattleController();

            NewUp(battleController);

            CreateCombatantsList(battleController);
            QueueCombatantOrder(battleController, false);

            var result = nextTurnState.Execute(battleController);

            Assert.IsInstanceOf<EnemyActionSelectState>(result);
        }

        [Test]
        public void ExecuteClearsActionData()
        {
            var nextTurnState = new NextTurnState();
            var battleController = new BattleController();
            battleController.ActionData = new ActionData {
                ButtonAction = Constants.Rest
            };
            NewUp(battleController);
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>();
            battleController.AllCombatants.Enqueue(character);

            var result = nextTurnState.Execute(battleController);

            Assert.AreEqual(null, battleController.ActionData.ButtonAction);
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