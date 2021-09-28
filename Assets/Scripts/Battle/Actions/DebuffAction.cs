using Assets.Scripts.Battle.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class DebuffAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Debuff Action. Using " + actionData.SelectedSkill.Name);

            var debuffSucceeds = ActionUtilities.Instance.CalculateDebuffSuccess(actionData.Target);
            actionData.CurrentCombatantBattleData.DecreaseFocusPoints(actionData.SelectedSkill.FocusPointCost);

            if (debuffSucceeds)
            {
                actionData.SelectedSkill.EffectsToAdd.ForEach((effect) => { actionData.Target.GetComponent<CharacterBattleData>().AddStatusEffect(effect, actionData.SelectedSkill.StatusEffectTurnCount); });
                var activeStatusEffects = new List<ActiveStatusEffect>();
                activeStatusEffects.AddRange(actionData.Target.GetComponent<CharacterBattleData>().ActiveStatusEffects);
                foreach (var activeStatusEffect in activeStatusEffects)
                {
                    if (actionData.SelectedSkill.EffectsToRemove.Contains(activeStatusEffect.StatusEffect))
                    {
                        activeStatusEffect.EndStatusEffect(actionData.Target.GetComponent<CharacterBattleData>());
                    }
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
