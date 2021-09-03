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
            

            //FUTURE CARD -- execute recurring stress/focus/etc?

            var currentCombatantBattleData = controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>();

            if (currentCombatantBattleData.ActiveStatusEffects.Count > 0) ProcessStatusEffects(currentCombatantBattleData);



            return controller.NextTurn;
        }

        private void ProcessStatusEffects(CharacterBattleData currentCombatantBattleData)
        {
            ApplyEndOfTurnStatusEffects(currentCombatantBattleData);
            DecreaseStatusEffectTurnCounters(currentCombatantBattleData);
            RemoveExpiredStatusEffects(currentCombatantBattleData);
        }

        private void ApplyEndOfTurnStatusEffects(CharacterBattleData currentCombatantBattleData)
        {
           // throw new NotImplementedException();
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
                currentCombatantBattleData.RemoveStatusEffect(effect.StatusEffect);
                Debug.Log($"Effect Expired: {effect.StatusEffect}");
            }
        }
    }
}
