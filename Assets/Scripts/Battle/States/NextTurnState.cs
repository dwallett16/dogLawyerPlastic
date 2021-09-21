using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class NextTurnState : BattleState
    {
        public override BattleState Execute(BattleController controller)
        {
            InitializeState("NextTurnState");
            //Current combatant is shifted to the end of the turn order
            if(controller.ActionData.CurrentCombatant != null)
                controller.AllCombatants.Enqueue(controller.ActionData.CurrentCombatant);

            //Prepare next combatant
            PrepareNextCombatant(controller);

            Debug.Log("Next character type: " + controller.ActionData.CurrentCombatantBattleData.Type.ToString());

            if (controller.ActionData.CurrentCombatantBattleData.ActiveStatusEffects.Any(s => s.StatusEffect == controller.StunnedEffect))
            {
                Debug.Log(controller.ActionData.CurrentCombatantBattleData.DisplayName + " is Stunned. Skipping turn.");
                return controller.EndTurn;
            }

            if (controller.ActionData.CurrentCombatantBattleData.Type == CharacterType.PlayerCharacter)
            {
                controller.PlayerActionSelect.NewState = true;
                return controller.PlayerActionSelect;
            }
            else
            {
                controller.EnemyActionSelect.NewState = true;
                return controller.EnemyActionSelect;
            }
        }

        private void PrepareNextCombatant(BattleController controller)
        {
            controller.ActionData = new ActionData();
            controller.ActionData.CurrentCombatant = controller.AllCombatants.Dequeue();
            controller.ActionData.CurrentCombatantBattleData = controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>();
        }
    }
}
