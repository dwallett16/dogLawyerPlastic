using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class EndTurnState : BattleState
    {
        public override BattleState Execute(BattleController controller)
        {
            InitializeState("EndTurnState");

            var currentCombatantBattleData = controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>();

            if (currentCombatantBattleData.ActiveStatusEffects.Count > 0) ProcessStatusEffects(currentCombatantBattleData);

            if(controller.IsSubmitButtonPressed)
            {
                return controller.NextTurn;
            }
            return this;
        }

        private void ProcessStatusEffects(CharacterBattleData currentCombatantBattleData)
        {
            ApplyEndOfTurnStatusEffects(currentCombatantBattleData);
            DecreaseStatusEffectTurnCounters(currentCombatantBattleData);
            RemoveExpiredStatusEffects(currentCombatantBattleData);
        }

        private void ApplyEndOfTurnStatusEffects(CharacterBattleData currentCombatantBattleData)
        {
            foreach(var effect in currentCombatantBattleData.ActiveStatusEffects)
            {
                if (!effect.StatusEffect.NonstandardEffect && (effect.StatusEffect.IsRecurring || !effect.HasBeenApplied))
                {
                    effect.ApplyStandardEffect(currentCombatantBattleData);
                }
            }
        }

        private void DecreaseStatusEffectTurnCounters(CharacterBattleData currentCombatantBattleData)
        {
            foreach (var activeStatusEffect in currentCombatantBattleData.ActiveStatusEffects)
            {
                activeStatusEffect.DecreaseRoundsRemainingByOne();
                Debug.Log($"Effect Counter Decreased By One: {activeStatusEffect.StatusEffect}");
            }
        }

        private void RemoveExpiredStatusEffects(CharacterBattleData currentCombatantBattleData)
        {
            var expiredEffects = currentCombatantBattleData.ActiveStatusEffects.Where(x => x.RoundsRemaining <= 0);

            foreach (var effect in expiredEffects.ToList())
            {
                effect.EndStatusEffect(currentCombatantBattleData);
                Debug.Log($"Effect Expired: {effect.StatusEffect}");
            }
        }
    }
}
