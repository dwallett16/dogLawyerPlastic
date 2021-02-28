using System;
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

        private void CreateCombatantsList(BattleController battleController)
        {
            for (int i = 0; i < 2; i++)
            {
                var prosecutor = new GameObject();
                prosecutor.AddComponent<CharacterBattleData>();
                prosecutor.GetComponent<CharacterBattleData>().type = CharacterType.PlayerCharacter;
                battleController.prosecutors.Add(prosecutor);
            }
            for (int i = 0; i < 2; i++)
            {
                var defenseAttorney = new GameObject();
                defenseAttorney.AddComponent<CharacterBattleData>();
                defenseAttorney.GetComponent<CharacterBattleData>().type = CharacterType.DefenseCharacter;
                battleController.defenseAttorneys.Add(defenseAttorney);
            }
        }

        private void QueueCombatantOrder(BattleController battleController, bool isPlayerCharacterNext)
        {
            if (isPlayerCharacterNext)
            {
                battleController.allCombatants.Enqueue(battleController.prosecutors[0]);
                battleController.allCombatants.Enqueue(battleController.defenseAttorneys[0]);
            }
            else
            {
                battleController.allCombatants.Enqueue(battleController.defenseAttorneys[0]);
                battleController.allCombatants.Enqueue(battleController.prosecutors[0]);
            }
            battleController.allCombatants.Enqueue(battleController.prosecutors[1]);
            battleController.allCombatants.Enqueue(battleController.defenseAttorneys[1]);
        }

        private void NewUp(BattleController battleController)
        {
            battleController.allCombatants = new Queue<GameObject>();
            battleController.prosecutors = new List<GameObject>();
            battleController.defenseAttorneys = new List<GameObject>();
            battleController.PlayerActionSelect = new PlayerActionSelectState();
            battleController.Initial = new InitialState();
            battleController.EnemyActionSelect = new EnemyActionSelectState();
        }
    }
}
