using Assets.Scripts.Battle.Actions;
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
            CharacterBattleData currentCombatantBattleData = controller.ActionData.CurrentCombatantBattleData;
            if (NewState) {
                InitializeState("EnemyActionSelectState");
                if (currentCombatantBattleData != null)
                {
                    Debug.Log("Current Combatant: " + currentCombatantBattleData.name);
                }
            }

            var filteredSkills = currentCombatantBattleData.Skills.Where(x => x.FocusPointCost <= currentCombatantBattleData.CurrentFocusPoints);

            if(!filteredSkills.Any())
            {
                controller.ActionData.Action = new RestAction();
                controller.Action.NewState = true;
                return controller.Action;
            }

            controller.ActionData.Action = new StressAttackAction();
            controller.ActionData.SelectedSkill = filteredSkills.First();
            controller.ActionData.Target = controller.Prosecutors[0];

            controller.Action.NewState = true;
            return controller.Action;
        }
    }
}
