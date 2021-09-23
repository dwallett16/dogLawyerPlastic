using Assets.Scripts.Battle.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.States
{
    public class EnemyActionSelectState : BattleState
    {
        private readonly IProbabilityHelper ProbabilityHelper;
        public EnemyActionSelectState(IProbabilityHelper probabilityHelper)
        {
            ProbabilityHelper = probabilityHelper;
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

            if (!filteredSkills.Any())
            {
                controller.ActionData.Action = new RestAction();
                controller.Action.NewState = true;
                return controller.Action;
            }

            var probabilityRanges = CreateSkillProbabilityList(currentCombatantBattleData, filteredSkills);
            controller.ActionData.SelectedSkill = SelectSkillBasedOnProbability(probabilityRanges);

            controller.ActionData.Action = new StressAttackAction();
            controller.ActionData.Target = controller.Prosecutors[0];

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
