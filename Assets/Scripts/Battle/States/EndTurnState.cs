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

            foreach(var activeStatusEffect in currentCombatantBattleData.ActiveStatusEffects)
            {
                activeStatusEffect.DecreaseRoundsRemainingByOne();
                Debug.Log($"Effect Decreased By One: {activeStatusEffect.StatusEffect}");
            }

            var expiredEffects = currentCombatantBattleData.ActiveStatusEffects.Where(x => x.RoundsRemaining <= 0);

            foreach (var effect in expiredEffects.ToList())
            {
                currentCombatantBattleData.RemoveStatusEffect(effect.StatusEffect);
                Debug.Log($"Effect Expired: {effect.StatusEffect}");
            }

            return controller.NextTurn;
        }
    }
}
