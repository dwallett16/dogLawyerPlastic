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
            controller.ActionData = new ActionData();
            if (controller.AllCombatants.Peek().GetComponent<CharacterBattleData>().type == CharacterType.PlayerCharacter)
            {
                Debug.Log("Queue Peek() next character type: " + CharacterType.PlayerCharacter.ToString());
                return controller.PlayerActionSelect;
            }
            else
            {
                Debug.Log("Queue Peek() next character type: " + CharacterType.DefenseCharacter.ToString());
                return controller.EnemyActionSelect;
            }
        }

        public void InitializeState(BattleController controller)
        {
            Debug.Log("CurrentState: NextTurnState");
        }
    }
}
