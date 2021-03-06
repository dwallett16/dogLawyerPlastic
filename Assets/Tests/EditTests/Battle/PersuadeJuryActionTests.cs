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
    class PersuadeJuryActionTests : EditTestBase
    {
        [Test]
        public void PersuadeJuryActionChangesJuryPoints()
        {
            var persuadeJuryAction = new PersuadeJuryAction();
            var target = new GameObject();
            var currentCombatant = new GameObject();
            var skill = new Skill();
            skill.Type = SkillTypes.Passion;
            skill.Power = 10;
            skill.FocusPointCost = 10;

            var targetData = target.AddComponent<JuryController>();
            targetData.CreateJuryData(10, 5);

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
            utilities.CalculateJuryPoints(Arg.Any<GameObject>(), Arg.Any<Skill>()).Returns(20);
            SetActionUtilitiesMock(utilities);

            persuadeJuryAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.CurrentFocusPoints);
            Assert.AreEqual(20, targetData.GetJuryPoints());
        }
    }
}
