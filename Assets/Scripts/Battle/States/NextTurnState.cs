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
            controller.ActionData = new ActionData();
            controller.ActionData.CurrentCombatant = controller.AllCombatants.Dequeue();
            controller.ActionData.CurrentCombatantBattleData = controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>();

            Debug.Log("Next character type: " + controller.ActionData.CurrentCombatantBattleData.type.ToString());

            if (controller.ActionData.CurrentCombatantBattleData.type == CharacterType.PlayerCharacter)
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
    }
}
