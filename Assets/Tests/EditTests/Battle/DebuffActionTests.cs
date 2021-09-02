using Assets.Scripts.Battle.Actions;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Tests.EditTests;
using Assets.Scripts.Battle;

namespace Battle
{
    class DebuffActionTests : EditTestBase
    {
        [Test]
        public void SuccessfulDebuffAddsStatusEffectToTarget()
        {
            var debuffAction = new DebuffAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Passion;
            skill.Power = 10;
            skill.FocusPointCost = 10;
            skill.EffectsToAdd = new List<StatusEffects>();
            skill.EffectsToRemove = new List<StatusEffects>();
            skill.EffectsToAdd.Add(StatusEffects.Embarrassed);
            skill.StatusEffectTurnCount = 1;

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.resistance = 10;
            targetData.IncreaseStress(10);

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.focusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.passion = 10;

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };

            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateDebuffSuccess(Arg.Any<GameObject>()).Returns(true);
            SetActionUtilitiesMock(utilities);

            List<ActiveStatusEffect> expectedEffects = new List<ActiveStatusEffect> { new ActiveStatusEffect(StatusEffects.Embarrassed, 1) };

            debuffAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(expectedEffects[0].StatusEffect, targetData.ActiveStatusEffects[0].StatusEffect);
        }

        [Test]
        public void DebuffMisses()
        {
            var debuffAction = new DebuffAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Persuasion;
            skill.Power = 10;
            skill.FocusPointCost = 10;
            skill.EffectsToAdd = new List<StatusEffects>();
            skill.EffectsToAdd.Add(StatusEffects.Embarrassed);

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.IncreaseStress(10);

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.focusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.persuasion = 10;

            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateDebuffSuccess(Arg.Any<GameObject>()).Returns(false);

            List<ActiveStatusEffect> expectedEffects = new List<ActiveStatusEffect>();

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };
            SetActionUtilitiesMock(utilities);

            debuffAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(expectedEffects, targetData.ActiveStatusEffects);
        }
    }
}
