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

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());

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

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());
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
            var ampedEffect = new StatusEffect { Name = "Amped" };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 1);
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(ampedEffect, 1);

            var result = state.Execute(controller);

            Assert.Zero(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects.Count);
        }

        [Test]
        public void ExecuteAppliesRecurringActiveStatusEffect()
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

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed", IsRecurring = true, StressPointAdjustment = 10 };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 3);
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects[0].ApplyStandardEffect(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>());

            var result = state.Execute(controller);

            Assert.AreEqual(20, controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().CurrentStress);
        }

        [Test]
        public void ExecuteDoesNotApplyNonRecurringStatusEffectThatHasAlreadyBeenApplied()
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

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed", IsRecurring = false, StressPointAdjustment = 10 };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 3);
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects[0].ApplyStandardEffect(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>());
            
            var result = state.Execute(controller);

            Assert.AreEqual(10, controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().CurrentStress);
        }

        [Test]
        public void ExecuteRestoresAttributesFromExpiredStatusEffectIfDoNotRestoreIsNotChecked()
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

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed", IsRecurring = false, EnduranceAdjustment = 10 };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 1);
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects[0].ApplyStandardEffect(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>());

            var result = state.Execute(controller);

            Assert.AreEqual(0, controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().CurrentEndurance);
        }

        [Test]
        public void ExecuteDoesNotRestoreAttributesFromExpiredStatusEffectIfDoNotRestoreIsChecked()
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

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed", IsRecurring = false, DoNotRestoreOnExpiration = true, EnduranceAdjustment = 10 };

            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().InitializeCharacter(new Character());
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().AddStatusEffect(embarrassedEffect, 1);
            controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().ActiveStatusEffects[0].ApplyStandardEffect(controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>());

            var result = state.Execute(controller);

            Assert.AreEqual(10, controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().CurrentEndurance);
        }

        private void NewUp(BattleController battleController)
        {
            battleController.AllCombatants = new Queue<GameObject>();
            battleController.Prosecutors = new List<GameObject>();
            battleController.DefenseAttorneys = new List<GameObject>();
            battleController.PlayerActionSelect = new PlayerActionSelectState();
            battleController.Initial = new InitialState();
            battleController.EnemyActionSelect = new EnemyActionSelectState(new ProbabilityHelper());
            battleController.battleData = new BattleData();
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
    }
}
