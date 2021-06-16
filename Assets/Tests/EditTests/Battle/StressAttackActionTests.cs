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
    class StressAttackActionTests
    {
        [Test]
        public void SuccessfulPassionAttackIncreasesTargetStressValue()
        {
            var stressAttackAction = new StressAttackAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Passion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.resistance = 10;
            targetData.currentStress = 10;

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.focusPointCapacity = 100;
            currentCombatantData.currentFocusPoints = 100;
            currentCombatantData.passion = 10;

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };

            stressAttackAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(30, targetData.currentStress);
        }

        [Test]
        public void SuccessfulPersuasionAttackIncreasesTargetStressValue()
        {
            var stressAttackAction = new StressAttackAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Persuasion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.resistance = 10;
            targetData.currentStress = 10;

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.focusPointCapacity = 100;
            currentCombatantData.currentFocusPoints = 100;
            currentCombatantData.persuasion = 10;

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };

            stressAttackAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(30, targetData.currentStress);
        }

        [Test]
        public void StressAttackMisses()
        {
            var stressAttackAction = new StressAttackAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Persuasion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.currentStress = 10;

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.focusPointCapacity = 100;
            currentCombatantData.currentFocusPoints = 100;
            currentCombatantData.persuasion = 10;

            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateAttackSuccess(Arg.Any<GameObject>()).Returns(false);

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };
            actionData.ActionUtilities = utilities;

            stressAttackAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(10, targetData.currentStress);
        }
    }
}
