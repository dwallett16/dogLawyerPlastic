using Assets.Scripts.Battle.Actions;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Battle
{
    class DebuffActionTests
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

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.resistance = 10;
            targetData.IncreaseStress(10);
            targetData.activeStatusEffects = new List<StatusEffects>();

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

            List<StatusEffects> expectedEffects = new List<StatusEffects> { StatusEffects.Embarrassed };

            debuffAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(expectedEffects, targetData.activeStatusEffects);
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
            targetData.activeStatusEffects = new List<StatusEffects>();

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.focusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.persuasion = 10;

            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateDebuffSuccess(Arg.Any<GameObject>()).Returns(false);

            List<StatusEffects> expectedEffects = new List<StatusEffects>();

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };
            actionData.ActionUtilities = utilities;

            debuffAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(expectedEffects, targetData.activeStatusEffects);
        }
    }
}
