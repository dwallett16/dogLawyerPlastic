using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Assets.Scripts.Battle.States;

namespace Battle {
    public class PlayerActionStateTests
    {
        [Test]
        public void ExecuteReturnsEndTurnState() {
            var playerActionState = new ActionState();
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            controller.ActionData = new ActionData {
                Action = new RestAction(),
                CurrentCombatant = controller.AllCombatants.Dequeue()
            };
            controller.AllCombatants = new Queue<GameObject>();
            controller.EndTurn = new EndTurnState();

            var result = playerActionState.Execute(controller);

            Assert.IsInstanceOf<EndTurnState>(result);
        }

        private void CreateCombatantsList(BattleController battleController)
        {
            for (int i = 0; i < 2; i++)
            {
                var prosecutor = new GameObject();
                prosecutor.AddComponent<CharacterBattleData>();
                prosecutor.GetComponent<CharacterBattleData>().Type = CharacterType.PlayerCharacter;
                battleController.Prosecutors.Add(prosecutor);
            }
            for (int i = 0; i < 2; i++)
            {
                var defenseAttorney = new GameObject();
                defenseAttorney.AddComponent<CharacterBattleData>();
                defenseAttorney.GetComponent<CharacterBattleData>().Type = CharacterType.DefenseCharacter;
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
            battleController.EnemyActionSelect = new EnemyActionSelectState(new ProbabilityHelper());
            battleController.NextTurn = new NextTurnState();
        }
    }
}
