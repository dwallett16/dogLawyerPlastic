using Assets.Scripts.Battle.Actions;
using Assets.Scripts.Battle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class EnemyActionSelectState : BattleState
    {
        private readonly IProbabilityHelper ProbabilityHelper;
        private readonly IAiUtilities AiUtilities;
        public EnemyActionSelectState(IProbabilityHelper probabilityHelper, IAiUtilities aiUtilities)
        {
            ProbabilityHelper = probabilityHelper;
            AiUtilities = aiUtilities;
        }
        public override BattleState Execute(BattleController controller)
        {
            CharacterBattleData currentCombatantBattleData = controller.ActionData.CurrentCombatantBattleData;
            if (NewState)
            {
                InitializeState("EnemyActionSelectState");
                if (currentCombatantBattleData != null)
                {
                    Debug.Log("Current Combatant: " + currentCombatantBattleData.name);
                }
            }

            var filteredSkills = currentCombatantBattleData.Skills.Where(x => x.FocusPointCost <= currentCombatantBattleData.CurrentFocusPoints);

            //do we want to keep resting until FP is at a certain amount?
            if (!filteredSkills.Any())
            {
                controller.ActionData.Action = new RestAction();
                controller.Action.NewState = true;
                return controller.Action;
            }
            //when reordering, order of conditions matters in final result
            foreach(var condition in controller.ActionData.CurrentCombatantBattleData.Personality.Conditions)
            {
                if(AiUtilities.ProcessCondition(condition, controller))
                {
                    var priorities = controller.ActionData.CurrentCombatantBattleData.Personality.Priorities;
                    var replaceIndex = Array.IndexOf(priorities, condition.AffectedPriority);
                    var reorderedPriorities = new ActionTypes[priorities.Length];
                    Array.Copy(priorities, reorderedPriorities, priorities.Length);
                    for(var i = 0; i < replaceIndex; i++)
                    {
                        reorderedPriorities[i + 1] = priorities[i];
                    }
                    reorderedPriorities[0] = condition.AffectedPriority;
                    controller.ActionData.CurrentCombatantBattleData.Personality.Priorities = reorderedPriorities;
                }
            }

            var probabilityRanges = CreateSkillProbabilityList(currentCombatantBattleData, filteredSkills);
            controller.ActionData.SelectedSkill = SelectSkillBasedOnProbability(probabilityRanges);

            ActionUtilities.Instance.SetAction(controller.ActionData);
            //todo revisit this
            controller.ActionData.Target = controller.Prosecutors[ProbabilityHelper.GenerateNumberInRange(0, controller.Prosecutors.Count - 1)];

            controller.Action.NewState = true;
            return controller.Action;
        }

        private KeyValuePair<int, Skill>[] CreateSkillProbabilityList(CharacterBattleData currentCombatantBattleData, IEnumerable<Skill> filteredSkills)
        {
            var probabilityRanges = new KeyValuePair<int, Skill>[filteredSkills.Count()];
            int endRange = 0;
            var filteredSkillIndex = 0;
            foreach (var skill in filteredSkills)
            {
                var probability = skill.Likelihood / (Array.IndexOf(currentCombatantBattleData.Personality.Priorities, skill.ActionType) + 1);
                endRange += probability;
                probabilityRanges[filteredSkillIndex] = new KeyValuePair<int, Skill>(endRange, skill);
                filteredSkillIndex++;
            }

            return probabilityRanges;
        }

        private Skill SelectSkillBasedOnProbability(KeyValuePair<int, Skill>[] probabilityRanges)
        {
            var randomNumber = ProbabilityHelper.GenerateNumberInRange(1, probabilityRanges[probabilityRanges.Length - 1].Key);
            for (var probabilityIndex = 0; probabilityIndex < probabilityRanges.Length; probabilityIndex++)
            {
                var previousRange = probabilityIndex == 0 ? 0 : probabilityRanges[probabilityIndex - 1].Key;
                if (randomNumber > previousRange && randomNumber <= probabilityRanges[probabilityIndex].Key)
                {
                    return probabilityRanges[probabilityIndex].Value;
                }
            }
            return null;
        }
    }
}
