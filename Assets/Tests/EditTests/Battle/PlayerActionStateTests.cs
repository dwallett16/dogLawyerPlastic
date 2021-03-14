using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Assets.Scripts.Battle.States;

namespace Battle {
    public class PlayerActionStateTests
    {
        [Test]
        public void ExecuteReturnsNextTurnState() {
            var playerActionState = new ActionState();
            var controller = new BattleController();
            controller.ActionData = new ActionData();
            controller.AllCombatants = new Queue<GameObject>();
            controller.NextTurn = new NextTurnState();

            var result = playerActionState.Execute(controller);

            Assert.IsInstanceOf<NextTurnState>(result);
        }

        [Test]
        public void ExecuteReplenishesFocusPointsToCurrentTurnCharacterWhenButtonActionRest() {
            var playerActionState = new ActionState();
            var controller = new BattleController();
            controller.ActionData = new ActionData {
                ButtonAction = Constants.Rest
            };
            controller.AllCombatants = new Queue<GameObject>();
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>().currentFocusPoints = 10;
            controller.ActionData.CurrentCombatant = character;

            playerActionState.Execute(controller);

            Assert.IsTrue(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints > 10);
        }

        [Test]
        public void ExecutePutsCurrentCombatantToEndOfQueue() {
            var playerActionState = new ActionState();
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            controller.ActionData = new ActionData();
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>().currentFocusPoints = 10;
            controller.ActionData.CurrentCombatant = character;

            playerActionState.Execute(controller);

            Assert.AreEqual(character, controller.AllCombatants.ToArray()[controller.AllCombatants.Count-1]);
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
