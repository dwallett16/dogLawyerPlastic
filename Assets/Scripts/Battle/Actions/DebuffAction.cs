using System;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class DebuffAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Debuff Action");

            var debuffSucceeds = ActionUtilities.Instance.CalculateDebuffSuccess(actionData.Target);
            actionData.CurrentCombatantBattleData.DecreaseFocusPoints(actionData.SelectedSkill.FocusPointCost);

            if (debuffSucceeds)
            {
                actionData.SelectedSkill.EffectsToAdd.ForEach((effect) => { actionData.Target.GetComponent<CharacterBattleData>().AddStatusEffect(effect); });
                foreach (var effect in actionData.SelectedSkill.EffectsToRemove)
                {
                    actionData.Target.GetComponent<CharacterBattleData>().RemoveStatusEffect(effect);
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
