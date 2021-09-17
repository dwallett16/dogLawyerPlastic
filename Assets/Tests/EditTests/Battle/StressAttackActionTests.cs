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
using Assets.Scripts.Battle.Utilities;

namespace Battle
{
    class StressAttackActionTests: EditTestBase
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
            targetData.Resistance = 10;
            targetData.StressCapacity = 100;
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
            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateStressAttackPower(Arg.Any<GameObject>(), Arg.Any<Skill>()).Returns(20);
            utilities.CalculateAttackSuccess(Arg.Any<GameObject>()).Returns(true);
            SetActionUtilitiesMock(utilities);

            stressAttackAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(30, targetData.CurrentStress);
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
            targetData.Resistance = 10;
            targetData.StressCapacity = 100;
            targetData.IncreaseStress(10);

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.FocusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.Persuasion = 10;

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };
            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateStressAttackPower(Arg.Any<GameObject>(), Arg.Any<Skill>()).Returns(20);
            utilities.CalculateAttackSuccess(Arg.Any<GameObject>()).Returns(true);
            SetActionUtilitiesMock(utilities);

            stressAttackAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(30, targetData.CurrentStress);
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
            targetData.StressCapacity = 100;
            targetData.IncreaseStress(10);

            var currentCombatantData = currentCombatant.AddComponent<CharacterBattleData>();
            currentCombatantData.FocusPointCapacity = 100;
            currentCombatantData.IncreaseFocusPoints(100);
            currentCombatantData.Persuasion = 10;

            var utilities = Substitute.For<IActionUtilities>();
            utilities.CalculateAttackSuccess(Arg.Any<GameObject>()).Returns(false);

            var actionData = new ActionData()
            {
                CurrentCombatant = currentCombatant,
                CurrentCombatantBattleData = currentCombatantData,
                Target = target,
                SelectedSkill = skill
            };
            SetActionUtilitiesMock(utilities);

            stressAttackAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(10, targetData.CurrentStress);
        }
    }
}
