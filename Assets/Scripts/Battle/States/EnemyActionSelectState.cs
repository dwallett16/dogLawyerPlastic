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

            Skill selectedSkill = null;
            foreach(var skill in currentCombatantBattleData.Skills)
            {
                if(currentCombatantBattleData.CurrentFocusPoints >= skill.FocusPointCost)
                {
                    selectedSkill = skill;
                    break;
                }
            }

            if(selectedSkill == null)
            {
                controller.ActionData.Action = new RestAction();
            }
            else
            {
                controller.ActionData.Action = new StressAttackAction();
                controller.ActionData.SelectedSkill = selectedSkill;
                controller.ActionData.Target = controller.Prosecutors[0];
            }

            controller.Action.NewState = true;
            return controller.Action;
        }
    }
}
