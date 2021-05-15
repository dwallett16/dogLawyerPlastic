using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class StressRecoveryAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Stress Recovery Action");

            actionData.CurrentCombatantBattleData.currentFocusPoints -= actionData.SelectedSkill.FocusPointCost;


            actionData.Target.GetComponent<CharacterBattleData>().currentStress -= ActionUtilities.CalculateStressRecoveryPower(actionData.CurrentCombatant, actionData.SelectedSkill);

            if (actionData.Target.GetComponent<CharacterBattleData>().currentStress < 0)
            {
                actionData.Target.GetComponent<CharacterBattleData>().currentStress = 0;
            }

            Debug.Log("Recovery, Target Stress: " + actionData.Target.GetComponent<CharacterBattleData>().currentStress);
        }
    }
}
