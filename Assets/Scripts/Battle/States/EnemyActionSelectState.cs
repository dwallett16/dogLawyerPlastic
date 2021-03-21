using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class EnemyActionSelectState : BattleState
    {
        public override BattleState Execute(BattleController controller)
        {
            if (NewState) {
                InitializeState("EnemyActionSelectState");
                var currentCombatantBattleData = controller.ActionData.CurrentCombatant?.GetComponent<CharacterBattleData>();
                if (currentCombatantBattleData != null)
                {
                    Debug.Log("Current Combatant: " + currentCombatantBattleData.name);
                }
            }

            return this;
        }
    }
}
