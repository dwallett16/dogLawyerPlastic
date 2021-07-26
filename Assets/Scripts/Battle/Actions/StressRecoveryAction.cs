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

            actionData.CurrentCombatantBattleData.DecreaseFocusPoints(actionData.SelectedSkill.FocusPointCost);

            actionData.Target.GetComponent<CharacterBattleData>().ReduceStress(ActionUtilities.Instance.CalculateStressRecoveryPower(actionData.CurrentCombatant, actionData.SelectedSkill));

            Debug.Log("Recovery, Target Stress: " + actionData.Target.GetComponent<CharacterBattleData>().currentStress);
        }
    }
}
