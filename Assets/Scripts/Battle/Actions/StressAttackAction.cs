using System;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class StressAttackAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Stress Attack Action");

            var attackSucceeds = ActionUtilities.CalculateAttackSuccess(actionData.Target);
            actionData.CurrentCombatantBattleData.currentFocusPoints -= actionData.SelectedSkill.FocusPointCost;

            if (attackSucceeds)
            {
                actionData.Target.GetComponent<CharacterBattleData>().currentStress += ActionUtilities.CalculateStressAttackPower(actionData.CurrentCombatant, actionData.SelectedSkill);
                Debug.Log("Attack Success, Target Stress: " + actionData.Target.GetComponent<CharacterBattleData>().currentStress);
            }
            else
            {
                Debug.Log("Attack Failure");
            }
        }
    }
}
