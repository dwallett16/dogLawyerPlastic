using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Battle.States;
using Assets.Scripts.Battle.Utilities;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using NUnit.Framework;
using UnityEngine;

namespace Battle
{
    public class NextTurnStateTests
    {
        [Test]
        public void ExecuteSetsCurrentCombatantFromQueue() {
            var state = new NextTurnState();
            state.NewState = true;
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);
            controller.ActionData = new ActionData();
            controller.Action = new ActionState();

            state.Execute(controller);

            Assert.AreEqual(controller.Prosecutors[0], controller.ActionData.CurrentCombatant);
        }

        [Test]
        public void ExecutePutsCurrentCombatantToEndOfQueue()
        {
            var nextTurnState = new NextTurnState();
            var controller = new BattleController();
            NewUp(controller);
            CreateCombatantsList(controller);
            QueueCombatantOrder(controller, true);

            var character = new GameObject();
            character.AddComponent<CharacterBattleData>().IncreaseFocusPoints(10);
            controller.ActionData = new ActionData
            {
                Action = new RestAction()
            };
            controller.ActionData.CurrentCombatant = character;


            nextTurnState.Execute(controller);

            Assert.AreEqual(character, controller.AllCombatants.ToArray()[controller.AllCombatants.Count - 1]);
        }

        [Test]
        public void ExecuteReturnsPlayerActionSelectStateIfPlayerCharacterIsNextInTurnOrder()
        {
            var nextTurnState = new NextTurnState();
            var battleController = new BattleController();

            NewUp(battleController);

            CreateCombatantsList(battleController);
            QueueCombatantOrder(battleController, true);

            battleController.ActionData = new ActionData
            {
                Action = new RestAction(),
                CurrentCombatant = new GameObject()
            };

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

            battleController.ActionData = new ActionData
            {
                Action = new RestAction(),
                CurrentCombatant = new GameObject()
            };

            var result = nextTurnState.Execute(battleController);

            Assert.IsInstanceOf<EnemyActionSelectState>(result);
        }

        [Test]
        public void ExecuteReturnsEndTurnStateIfNextCombatantIsStunned()
        {
            var nextTurnState = new NextTurnState();
            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);
            QueueCombatantOrder(battleController, false);

            battleController.ActionData = new ActionData
            {
                Action = new RestAction(),
                CurrentCombatant = new GameObject()
            };

            battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>().AddStatusEffect(battleController.StunnedEffect, 3);

            var result = nextTurnState.Execute(battleController);

            Assert.IsInstanceOf<EndTurnState>(result);
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
            CreateCombatantsList(battleController);
            QueueCombatantOrder(battleController, false);
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>();
            battleController.AllCombatants.Enqueue(character);

            nextTurnState.Execute(battleController);

            Assert.AreEqual(null, battleController.ActionData.ButtonAction);
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
            battleController.EnemyActionSelect = new EnemyActionSelectState(new ProbabilityHelper(), new AiUtilities());
            battleController.EndTurn = new EndTurnState();
            battleController.battleData = new BattleData();
            battleController.StunnedEffect = new StatusEffect { Name = "Stunned" };
        }
    }
}
