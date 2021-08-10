using Assets.Tests.EditTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Assets.Scripts.Battle.States;
using UnityEngine;
using Assets.Scripts.Battle.Actions;

namespace Battle
{
    public class EnemyActionSelectStateTests: EditTestBase
    {
        [Test]
        public void ExecuteSetsRestActionWhenNotEnoughFpToSelectSkill()
        {
            var enemyActionState = new EnemyActionSelectState();
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.DecreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.IsInstanceOf<RestAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteSelectsSkillWhenEnoughFp()
        {
            var enemyActionState = new EnemyActionSelectState();
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(0, controller.ActionData.SelectedSkill.Id);
            Assert.IsInstanceOf<StressAttackAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteReturnsActionState()
        {
            var enemyActionState = new EnemyActionSelectState();
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            var result = enemyActionState.Execute(controller);

            Assert.IsInstanceOf<ActionState>(result);
        }

        private BattleController SetupController()
        {
            var enemyCharacter = new CharacterBattleData
            {
                skills = new List<Skill>
                {
                    TestDataFactory.CreateSkill(0, fpCost: 5),
                    TestDataFactory.CreateSkill(1, fpCost: 3)
                },
                focusPointCapacity = 10
            };

            var prosecutors = new List<GameObject>() { new GameObject() };

            return new BattleController
            {
                ActionData = new ActionData
                {
                    CurrentCombatantBattleData = enemyCharacter
                },
                Action = new ActionState(),
                Prosecutors = prosecutors
            };
        }
    }
}
