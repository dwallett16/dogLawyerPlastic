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
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.DecreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.IsInstanceOf<RestAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteSelectsSkillWhenEnoughFp()
        {
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.IsNotNull(controller.ActionData.SelectedSkill);
            Assert.IsInstanceOf<StressAttackAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteReturnsActionState()
        {
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            var result = enemyActionState.Execute(controller);

            Assert.IsInstanceOf<ActionState>(result);
        }

        [Test]
        public void ExecuteSetsBuffSkillBasedOnPriority()
        {
            var probabilityMock = Substitute.For<IProbabilityHelper>();
            probabilityMock.GenerateNumberInRange(1, 71).Returns(1);
            var enemyActionState = new EnemyActionSelectState(probabilityMock);
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(1, controller.ActionData.SelectedSkill.Id);
        }

        [Test]
        public void ExecuteSetsPersuadeJurySkillBasedOnPriority()
        {
            var probabilityMock = Substitute.For<IProbabilityHelper>();
            probabilityMock.GenerateNumberInRange(1, 71).Returns(17);
            var enemyActionState = new EnemyActionSelectState(probabilityMock);
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(2, controller.ActionData.SelectedSkill.Id);
        }

        [Test]
        public void ExecuteSetsStressAttackSkillBasedOnPriority()
        {
            var probabilityMock = Substitute.For<IProbabilityHelper>();
            probabilityMock.GenerateNumberInRange(1, 71).Returns(70);
            var enemyActionState = new EnemyActionSelectState(probabilityMock);
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(0, controller.ActionData.SelectedSkill.Id);
        }

        private BattleController SetupController()
        {
            var enemyCharacter = new CharacterBattleData
            {
                Skills = new List<Skill>
                {
                    TestDataFactory.CreateSkill(1, fpCost: 3, likelihood: 10, actionType: ActionTypes.Buff),
                    TestDataFactory.CreateSkill(2, fpCost: 3, likelihood: 50, actionType: ActionTypes.PersuadeJury),
                    TestDataFactory.CreateSkill(0, fpCost: 5, likelihood: 50, actionType: ActionTypes.StressAttack)
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
