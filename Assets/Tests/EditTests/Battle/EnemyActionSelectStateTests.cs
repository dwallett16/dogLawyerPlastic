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

        [Test]
        public void ExecuteSetsSkillBasedOnPriority()
        {
            SetupController();
        }

        private BattleController SetupController()
        {
            var enemyCharacter = new CharacterBattleData
            {
                Skills = new List<Skill>
                {
                    TestDataFactory.CreateSkill(0, fpCost: 5, actionType: ActionTypes.StressAttack),
                    TestDataFactory.CreateSkill(1, fpCost: 3, actionType: ActionTypes.Buff),
                    TestDataFactory.CreateSkill(2, fpCost: 3, actionType: ActionTypes.PersuadeJury),
                    TestDataFactory.CreateSkill(3, fpCost: 3, actionType: ActionTypes.Debuff),
                    TestDataFactory.CreateSkill(4, fpCost: 3, actionType: ActionTypes.StressRecovery)
                },
                FocusPointCapacity = 10,
                Personality = TestDataFactory.CreatePersonality(0, new ActionTypes[] { ActionTypes.StressAttack, ActionTypes.Buff, ActionTypes.PersuadeJury, ActionTypes.Debuff, ActionTypes.StressRecovery })
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
