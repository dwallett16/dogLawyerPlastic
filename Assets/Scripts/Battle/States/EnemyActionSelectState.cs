using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class EnemyActionSelectState : IBattleState
    {
        public bool newState;
        public IBattleState Execute(BattleController controller)
        {
            if (newState) InitializeState(controller);
            return this;
        }

        public void InitializeState(BattleController controller)
        {
            Debug.Log("Current State: EnemyActionSelectState");
            var currentCombatantBattleData = controller.CurrentCombatant?.GetComponent<CharacterBattleData>();
            if (currentCombatantBattleData != null)
            {
                Debug.Log("Current Combatant: " + currentCombatantBattleData.name);
            }
            newState = false;
        }
    }
}
