using Assets.Scripts.Battle.States;
using Assets.Scripts.Battle.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Battle
{
    class AiUtilitiesTests
    {
        [Test]
        public void ProcessCondition_NoAnds_OneActiveSubConditionIsTrue_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 10;
            condition.DefenseAttorney2.FocusPoints.Evaluate = true;
            condition.DefenseAttorney2.FocusPoints.Value = 5;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessCondition_NoAnds_OneActiveSubCondition_LessThanChecked_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 10;
            condition.Self.Stress.LessThan = true;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessCondition_DefenseTeamTotalStressSubConditionIsTrue_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.DefenseTeamStressTotal.Evaluate = true;
            condition.DefenseTeamStressTotal.Value = 10;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);
            battleController.DefenseAttorneys[1].GetComponent<CharacterBattleData>().IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessCondition_JuryPointsIsTrue_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.JuryPoints.Evaluate = true;
            condition.JuryPoints.Value = 10;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);
            battleController.Jury.GetComponent<JuryController>().ChangePoints(10);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessCondition_NoAnds_NoSubConditionIsTrue_ReturnsFalse()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 10;
            condition.DefenseAttorney2.FocusPoints.Evaluate = true;
            condition.DefenseAttorney2.FocusPoints.Value = 15;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsFalse(result);
        }

        [Test]
        public void ProcessCondition_AndsOnly_AllSubConditionsAreTrue_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 10;
            condition.Self.Stress.And = true;
            condition.DefenseAttorney2.FocusPoints.Evaluate = true;
            condition.DefenseAttorney2.FocusPoints.Value = 5;
            condition.DefenseAttorney2.FocusPoints.And = true;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(15);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessCondition_AndsOnly_OneSubConditionIsTrue_OneSubConditionIsFalse_ReturnsFalse()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 10;
            condition.Self.Stress.And = true;
            condition.DefenseAttorney2.FocusPoints.Evaluate = true;
            condition.DefenseAttorney2.FocusPoints.Value = 5;
            condition.DefenseAttorney2.FocusPoints.And = true;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsFalse(result);
        }

        [Test]
        public void ProcessCondition_AndsOrs_OneOrSubConditionIsTrue_OneAndSubConditionIsTrue_OneAndSubConditionIsFalse_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 10;
            condition.Self.Stress.And = true;
            condition.DefenseAttorney2.Stress.Evaluate = true;
            condition.DefenseAttorney2.Stress.Value = 10;
            condition.DefenseAttorney2.Stress.And = true;
            condition.DefenseAttorney2.FocusPoints.Evaluate = true;
            condition.DefenseAttorney2.FocusPoints.Value = 5;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessCondition_AndsOrs_OrSubConditionIsFalse_AllAndSubConditionsAreTrue_ReturnsTrue()
        {
            var aiUtilities = new AiUtilities();

            var battleController = new BattleController();

            NewUp(battleController);
            CreateCombatantsList(battleController);

            var condition = new Condition();
            condition.Self.Stress.Evaluate = true;
            condition.Self.Stress.Value = 5;
            condition.Self.Stress.And = true;
            condition.DefenseAttorney2.Stress.Evaluate = true;
            condition.DefenseAttorney2.Stress.Value = 0;
            condition.DefenseAttorney2.Stress.And = true;
            condition.DefenseAttorney2.FocusPoints.Evaluate = true;
            condition.DefenseAttorney2.FocusPoints.Value = 15;

            var selfBattleData = battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>();
            selfBattleData.IncreaseStress(5);

            var result = aiUtilities.ProcessCondition(condition, battleController, selfBattleData);
            Assert.IsTrue(result);
        }


        private void NewUp(BattleController battleController)
        {
            battleController.AllCombatants = new Queue<GameObject>();
            battleController.Prosecutors = new List<GameObject>();
            battleController.DefenseAttorneys = new List<GameObject>();
            battleController.PlayerActionSelect = new PlayerActionSelectState();
            battleController.Initial = new InitialState();
            battleController.EnemyActionSelect = new EnemyActionSelectState(new ProbabilityHelper(), new AiUtilities());
            battleController.battleData = new BattleData();

            battleController.Jury = new GameObject();
            battleController.Jury.AddComponent<JuryController>();
            battleController.Jury.GetComponent<JuryController>().CreateJuryData(5, 10);
        }

        private void CreateCombatantsList(BattleController battleController)
        {
            for (int i = 0; i < 2; i++)
            {
                var prosecutor = new GameObject();
                prosecutor.AddComponent<CharacterBattleData>();
                var characterData = TestDataFactory.CreateCharacter(i, CharacterType.PlayerCharacter);
                prosecutor.GetComponent<CharacterBattleData>().InitializeCharacter(characterData);
                battleController.Prosecutors.Add(prosecutor);
            }
            for (int i = 0; i < 2; i++)
            {
                var defenseAttorney = new GameObject();
                defenseAttorney.AddComponent<CharacterBattleData>();
                var characterData = TestDataFactory.CreateCharacter(i, CharacterType.DefenseCharacter);
                defenseAttorney.GetComponent<CharacterBattleData>().InitializeCharacter(characterData);
                battleController.DefenseAttorneys.Add(defenseAttorney);
            }
        }
    }
}
