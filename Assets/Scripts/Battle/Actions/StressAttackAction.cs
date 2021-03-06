using Assets.Scripts.Battle.Utilities;
using System;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class StressAttackAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Stress Attack Action. Using " + actionData.SelectedSkill.Name);

            var attackSucceeds = ActionUtilities.Instance.CalculateAttackSuccess(actionData.Target);
            actionData.CurrentCombatantBattleData.DecreaseFocusPoints(actionData.SelectedSkill.FocusPointCost);

            if (attackSucceeds)
            {
                actionData.Target.GetComponent<CharacterBattleData>().IncreaseStress(ActionUtilities.Instance.CalculateStressAttackPower(actionData.CurrentCombatant, actionData.SelectedSkill));
                Debug.Log("Attack Success, Target Stress: " + actionData.Target.GetComponent<CharacterBattleData>().CurrentStress);
            }
            else
            {
                Debug.Log("Attack Failure");
            }
        }
    }
}
