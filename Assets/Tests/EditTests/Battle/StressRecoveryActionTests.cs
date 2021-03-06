using Assets.Scripts.Battle.Actions;
using Assets.Scripts.Battle.Utilities;
using Assets.Tests.EditTests;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Battle
{
    class StressRecoveryActionTests : EditTestBase
    {
        [Test]
        public void StressRecoveryActionDecreasesStress()
        {
            var stressRecoveryAction = new StressRecoveryAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Passion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var targetData = target.AddComponent<CharacterBattleData>();
            targetData.StressCapacity = 100;
            targetData.IncreaseStress(100);

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
            utilities.CalculateStressRecoveryPower(Arg.Any<GameObject>(), Arg.Any<Skill>()).Returns(20);
            SetActionUtilitiesMock(utilities);

            stressRecoveryAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(80, targetData.CurrentStress);
        }
    }
}
