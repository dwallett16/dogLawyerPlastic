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
using Assets.Scripts.Battle.Utilities;

namespace Battle
{
    public class EnemyActionSelectStateTests: EditTestBase
    {
        [Test]
        public void ExecuteSetsRestActionWhenNotEnoughFpToSelectSkill()
        {
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper(), new AiUtilities());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.DecreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.IsInstanceOf<RestAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteSelectsSkillWhenEnoughFp()
        {
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper(), new AiUtilities());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.IsNotNull(controller.ActionData.SelectedSkill);
            Assert.IsInstanceOf<IAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteReturnsActionState()
        {
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper(), new AiUtilities());
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
            var enemyActionState = new EnemyActionSelectState(probabilityMock, new AiUtilities());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(1, controller.ActionData.SelectedSkill.Id);
            Assert.IsInstanceOf<BuffAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteSetsPersuadeJurySkillBasedOnPriority()
        {
            var probabilityMock = Substitute.For<IProbabilityHelper>();
            probabilityMock.GenerateNumberInRange(1, 71).Returns(17);
            var enemyActionState = new EnemyActionSelectState(probabilityMock, new AiUtilities());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(2, controller.ActionData.SelectedSkill.Id);
            Assert.IsInstanceOf<PersuadeJuryAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecuteSetsStressAttackSkillBasedOnPriority()
        {
            var probabilityMock = Substitute.For<IProbabilityHelper>();
            probabilityMock.GenerateNumberInRange(1, 71).Returns(70);
            var enemyActionState = new EnemyActionSelectState(probabilityMock, new AiUtilities());
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(0, controller.ActionData.SelectedSkill.Id);
            Assert.IsInstanceOf<StressAttackAction>(controller.ActionData.Action);
        }

        [Test]
        public void ExecutePriorityGetsAdjustedWhenProcessConditionReturnsTrue()
        {
            var utilitiesMock = Substitute.For<IAiUtilities>();
            utilitiesMock.ProcessCondition(Arg.Any<Condition>(), Arg.Any<BattleController>(), Arg.Any<CharacterBattleData>()).Returns(true);
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper(), utilitiesMock);
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.Personality.Conditions = new List<Condition> { new Condition { AffectedPriority = ActionTypes.StressRecovery } };
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(ActionTypes.StressRecovery, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[0]);
            Assert.AreEqual(ActionTypes.StressAttack, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[1]);
            Assert.AreEqual(ActionTypes.Buff, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[2]);
            Assert.AreEqual(ActionTypes.PersuadeJury, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[3]);
            Assert.AreEqual(ActionTypes.Debuff, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[4]);
        }

        [Test]
        public void ExecutePriorityGetsAdjustedWhenMultipleProcessConditionReturnTrue()
        {
            var utilitiesMock = Substitute.For<IAiUtilities>();
            utilitiesMock.ProcessCondition(Arg.Any<Condition>(), Arg.Any<BattleController>(), Arg.Any<CharacterBattleData>()).Returns(true);
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper(), utilitiesMock);
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.Personality.Conditions = new List<Condition> { new Condition { AffectedPriority = ActionTypes.StressRecovery }, new Condition { AffectedPriority = ActionTypes.PersuadeJury} };
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(ActionTypes.PersuadeJury, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[0]);
            Assert.AreEqual(ActionTypes.StressRecovery, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[1]);
            Assert.AreEqual(ActionTypes.StressAttack, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[2]);
            Assert.AreEqual(ActionTypes.Buff, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[3]);
            Assert.AreEqual(ActionTypes.Debuff, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[4]);
        }

        [Test]
        public void ExecutePriorityGetsAdjustedWhenSomeProcessConditionReturnTrue()
        {
            var utilitiesMock = Substitute.For<IAiUtilities>();
            utilitiesMock.ProcessCondition(Arg.Is<Condition>(cond => cond.AffectedPriority == ActionTypes.PersuadeJury), Arg.Any<BattleController>(), Arg.Any<CharacterBattleData>()).Returns(true);
            var enemyActionState = new EnemyActionSelectState(new ProbabilityHelper(), utilitiesMock);
            var controller = SetupController();
            controller.ActionData.CurrentCombatantBattleData.Personality.Conditions = new List<Condition> { new Condition { AffectedPriority = ActionTypes.StressRecovery }, new Condition { AffectedPriority = ActionTypes.PersuadeJury } };
            controller.ActionData.CurrentCombatantBattleData.IncreaseFocusPoints(10);

            enemyActionState.Execute(controller);

            Assert.AreEqual(ActionTypes.PersuadeJury, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[0]);
            Assert.AreEqual(ActionTypes.StressAttack, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[1]);
            Assert.AreEqual(ActionTypes.Buff, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[2]);
            Assert.AreEqual(ActionTypes.Debuff, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[3]);
            Assert.AreEqual(ActionTypes.StressRecovery, controller.ActionData.CurrentCombatantBattleData.Personality.Priorities[4]);
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
