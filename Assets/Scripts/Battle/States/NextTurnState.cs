using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class NextTurnState : IBattleState
    {
        public bool newState;
        public IBattleState Execute(BattleController controller)
        {
            if(newState) InitializeState(controller);
            Debug.Log("Queue Peek() next character type: " + controller.AllCombatants.Peek().GetComponent<CharacterBattleData>().type.ToString());
            
            controller.ActionData = new ActionData();
            if (controller.AllCombatants.Peek().GetComponent<CharacterBattleData>().type == CharacterType.PlayerCharacter)
            {
                return controller.PlayerActionSelect;
            }
            else
            {
                return controller.EnemyActionSelect;
            }
        }

        public void InitializeState(BattleController controller)
        {
            Debug.Log("CurrentState: NextTurnState");
        }
    }
}
