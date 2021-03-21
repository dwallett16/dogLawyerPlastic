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
            if(NewState) InitializeState("NextTurnState");
            Debug.Log("Queue Peek() next character type: " + controller.AllCombatants.Peek().GetComponent<CharacterBattleData>().type.ToString());
            
            controller.ActionData = new ActionData();
            if (controller.AllCombatants.Peek().GetComponent<CharacterBattleData>().type == CharacterType.PlayerCharacter)
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
