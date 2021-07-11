using System;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class PersuadeJuryAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Persuade Jury Action");
            actionData.CurrentCombatantBattleData.currentFocusPoints -= actionData.SelectedSkill.FocusPointCost;
            var pointsToAdd = actionData.ActionUtilities.CalculateJuryPoints(actionData.CurrentCombatant, actionData.SelectedSkill);
            actionData.Target.GetComponent<JuryController>().ChangePoints(pointsToAdd);
            Debug.Log("Current Jury Points: " + actionData.Target.GetComponent<JuryController>().GetJuryPoints());
        }
    }
}
