using Assets.Scripts.Battle.Actions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Battle
{
    class PersuadeJuryActionTests
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

            persuadeJuryAction.Act(actionData);

            Assert.AreEqual(90, currentCombatantData.currentFocusPoints);
            Assert.AreEqual(20, targetData.GetJuryPoints());
        }
    }
}
