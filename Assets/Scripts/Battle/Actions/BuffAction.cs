using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class BuffAction : IAction
    {
        public void Act(ActionData actionData)
        {
            Debug.Log("Buff Action. Using " + actionData.SelectedSkill.Name);

            actionData.CurrentCombatantBattleData.DecreaseFocusPoints(actionData.SelectedSkill.FocusPointCost);

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
            foreach (var effect in actionData.SelectedSkill.EffectsToRemove)
            {
                Debug.Log(effect.ToString());
            }
        }
    }
}
