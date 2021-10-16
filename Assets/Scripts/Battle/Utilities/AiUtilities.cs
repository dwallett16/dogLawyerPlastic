using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Battle.Utilities
{
    public class AiUtilities : IAiUtilities
    {
        public bool ProcessCondition(Condition condition, BattleController battleController, CharacterBattleData selfBattleData)
        {
            List<bool> andConditionResults = new List<bool>();

            var juryPoints = battleController.Jury.GetComponent<JuryController>().GetJuryPoints();

            var defenseAllyBattleDatas = new List<CharacterBattleData>();
            int defenseTotalStress = 0;
            int defenseTotalFocusPoints = 0;

            foreach (var defenseAttorney in battleController.DefenseAttorneys)
            {
                var characterBattleData = defenseAttorney.GetComponent<CharacterBattleData>();
                defenseTotalStress += characterBattleData.CurrentStress;
                defenseTotalFocusPoints += characterBattleData.CurrentFocusPoints;
                if (characterBattleData.DisplayName != selfBattleData.DisplayName) defenseAllyBattleDatas.Add(characterBattleData);
            }

            var prosecutorBattleDatas = new List<CharacterBattleData>();
            int prosecutionTotalStress = 0;
            int prosecutionTotalFocusPoints = 0;

            foreach(var prosecutor in battleController.Prosecutors)
            {
                var characterBattleData = prosecutor.GetComponent<CharacterBattleData>();
                prosecutionTotalStress += characterBattleData.CurrentStress;
                prosecutionTotalFocusPoints += characterBattleData.CurrentFocusPoints;
                prosecutorBattleDatas.Add(characterBattleData);
            }

            List<(SubCondition, int)> allSubConditionComparisons = new List<(SubCondition, int)>()
            {
                (condition.Self.Stress, selfBattleData.CurrentStress),
                (condition.Self.FocusPoints, selfBattleData.CurrentFocusPoints),
                (condition.DefenseAttorney2.Stress, defenseAllyBattleDatas[0].CurrentStress),
                (condition.DefenseAttorney2.FocusPoints, defenseAllyBattleDatas[0].CurrentFocusPoints),
                (condition.Prosecutor1.Stress, prosecutorBattleDatas[0].CurrentStress),
                (condition.Prosecutor1.FocusPoints, prosecutorBattleDatas[0].CurrentFocusPoints),
                (condition.Prosecutor2.Stress, prosecutorBattleDatas[1].CurrentStress),
                (condition.Prosecutor2.FocusPoints, prosecutorBattleDatas[1].CurrentFocusPoints),
                (condition.DefenseTeamStressTotal, defenseTotalStress),
                (condition.DefenseTeamFocusPointsTotal, defenseTotalFocusPoints),
                (condition.ProsecutorTeamStressTotal, prosecutionTotalStress),
                (condition.ProsecutorTeamFocusPointsTotal, prosecutionTotalFocusPoints),
                (condition.JuryPoints, juryPoints)
            };

            List<(SubCondition, int)> subConditionComparisons = allSubConditionComparisons.FindAll(x => x.Item1.Evaluate);

            foreach (var subConditionComparison in subConditionComparisons)
            {
                var subCondition = subConditionComparison.Item1;
                var comparisonValue = subConditionComparison.Item2;

                var subConditionResult = EvaluateSubCondition(subCondition, comparisonValue);
                
                if (!subCondition.And && subConditionResult == true) return true;

                if (subCondition.And) andConditionResults.Add(subConditionResult);
            }

            if (andConditionResults.Count > 0)
            {
                if (!andConditionResults.Contains(false)) return true;
            }

            return false;
        }

        private bool EvaluateSubCondition(SubCondition subCondition, int comparisonValue)
        {
            if (subCondition.LessThan)
            {
                if (comparisonValue <= subCondition.Value) return true;
            }
            else
            {
                if (comparisonValue >= subCondition.Value) return true;
            }

            return false;
        }
    }
}