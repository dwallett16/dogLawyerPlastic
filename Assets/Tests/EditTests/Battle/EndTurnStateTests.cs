using Assets.Scripts.Battle.States;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Battle
{
    public class EndTurnStateTests
    {
        [Test]
        public void ExecuteReturnsNextTurnState()
        {
            var state = new EndTurnState();
            var controller = new BattleController();
            controller.NextTurn = new NextTurnState();
            NewUp(controller);
            CreateCombatantsList(controller);
            controller.ActionData = new ActionData()
            {
                CurrentCombatant = controller.Prosecutors[0]
            };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().MapFromScriptableObject(new Character());

            var result = state.Execute(controller);

            Assert.IsInstanceOf<NextTurnState>(result);
        }

        [Test]
        public void ExecuteDecreasesActiveStatusEffectRoundsRemainingByOne()
        {
            var state = new EndTurnState();
            var controller = new BattleController();
            controller.NextTurn = new NextTurnState();
            NewUp(controller);
            CreateCombatantsList(controller);
            controller.ActionData = new ActionData()
            {
                CurrentCombatant = controller.Prosecutors[0]
            };

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed" };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().MapFromScriptableObject(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 3);

            var result = state.Execute(controller);

            Assert.AreEqual(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects[0].RoundsRemaining, 2);
        }

        [Test]
        public void ExecuteRemovesStatusEffectIfZeroRoundsAreRemaining()
        {
            var state = new EndTurnState();
            var controller = new BattleController();
            controller.NextTurn = new NextTurnState();
            NewUp(controller);
            CreateCombatantsList(controller);
            controller.ActionData = new ActionData()
            {
                CurrentCombatant = controller.Prosecutors[0]
            };

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed" };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().MapFromScriptableObject(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 1);

            var result = state.Execute(controller);

            Assert.Zero(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects.Count);
        }

        private void NewUp(BattleController battleController)
        {
            battleController.AllCombatants = new Queue<GameObject>();
            battleController.Prosecutors = new List<GameObject>();
            battleController.DefenseAttorneys = new List<GameObject>();
            battleController.PlayerActionSelect = new PlayerActionSelectState();
            battleController.Initial = new InitialState();
            battleController.EnemyActionSelect = new EnemyActionSelectState();
            battleController.battleData = new BattleData();
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
    }
}
