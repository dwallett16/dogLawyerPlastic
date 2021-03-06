using Assets.Scripts.Battle.Actions;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Battle;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;

namespace Battle
{
    class BuffActionTests
    {
        [Test]
        public void SuccessfulBuffRemovesStatusEffectFromTarget()
        {
            var buffAction = new BuffAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Passion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed" };

            skill.EffectsToAdd = new List<StatusEffect>();
            skill.EffectsToRemove = new List<StatusEffect>();
            skill.EffectsToRemove.Add(embarrassedEffect);

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.Resistance = 10;
            targetData.IncreaseStress(10);
            targetData.AddStatusEffect(embarrassedEffect, 2);

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.FocusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.Passion = 10;

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };

            List<ActiveStatusEffect> expectedEffects = new List<ActiveStatusEffect>();

            buffAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(expectedEffects, targetData.ActiveStatusEffects);
        }

        [Test]
        public void SuccessfulBuffAddsStatusEffectToTarget()
        {
            var buffAction = new BuffAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Passion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var embarrassedEffect = new StatusEffect { Name = "Embarrassed" };

            skill.EffectsToAdd = new List<StatusEffect>();
            skill.EffectsToRemove = new List<StatusEffect>();
            skill.EffectsToAdd.Add(embarrassedEffect);
            skill.StatusEffectTurnCount = 1;

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.Resistance = 10;
            targetData.IncreaseStress(10);

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.FocusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.Passion = 10;

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };

            List<ActiveStatusEffect> expectedEffects = new List<ActiveStatusEffect> { new ActiveStatusEffect(embarrassedEffect, 1) };

            buffAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(expectedEffects[0].StatusEffect, targetData.ActiveStatusEffects[0].StatusEffect);
        }
    }
}
