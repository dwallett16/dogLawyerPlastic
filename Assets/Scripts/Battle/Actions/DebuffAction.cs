using System;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class DebuffAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Debuff Action");

            var debuffSucceeds = actionData.ActionUtilities.CalculateDebuffSuccess(actionData.Target);
            actionData.CurrentCombatantBattleData.currentFocusPoints -= actionData.SelectedSkill.FocusPointCost;

            if (debuffSucceeds)
            {
                actionData.Target.GetComponent<CharacterBattleData>().activeStatusEffects.AddRange(actionData.SelectedSkill.EffectsToAdd);
                foreach(var effect in actionData.SelectedSkill.EffectsToRemove)
                {
                    actionData.Target.GetComponent<CharacterBattleData>().activeStatusEffects.Remove(effect);
                }
                Debug.Log("Debuff Success, Added Effects: ");
                foreach (var effect in actionData.SelectedSkill.EffectsToAdd)
                {
                    Debug.Log(effect.ToString());
                }
                Debug.Log("Removed effects: ");
                foreach(var effect in actionData.SelectedSkill.EffectsToRemove) {
                    Debug.Log(effect.ToString());
                }
            }
            else
            {
                Debug.Log("Debuff Failure");
            }
        }
    }
}
