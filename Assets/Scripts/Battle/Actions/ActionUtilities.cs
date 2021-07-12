using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public class ActionUtilities : IActionUtilities
    {
        public void SetAction(ActionData actionData)
        {
            switch (actionData.SelectedSkill.ActionType)
            {
                case ActionTypes.StressAttack:
                    actionData.Action = new StressAttackAction();
                    break;
                case ActionTypes.StressRecovery:
                    actionData.Action = new StressRecoveryAction();
                    break;
                case ActionTypes.Debuff:
                    actionData.Action = new DebuffAction();
                    break;
                case ActionTypes.Buff:
                    actionData.Action = new BuffAction();
                    break;
                case ActionTypes.PersuadeJury:
                    actionData.Action = new PersuadeJuryAction();
                    break;
            }
        }

        public bool CalculateAttackSuccess(GameObject target)
        {
            var data = target.GetComponent<CharacterBattleData>();

            if (data.resistance < 25)
            {
                return true;
            }
            return false;
        }

        public bool CalculateDebuffSuccess(GameObject target)
        {
            var data = target.GetComponent<CharacterBattleData>();

            if (data.resistance < 25)
            {
                return true;
            }
            return false;
        }

        public int CalculateStressAttackPower(GameObject actor, Skill skill)
        {
            var actorData = actor.GetComponent<CharacterBattleData>();

            if (skill.Type == SkillTypes.Passion)
                return actorData.passion + skill.Power;
            else
            {
                return actorData.persuasion + skill.Power;
            }
        }

        public int CalculateStressRecoveryPower(GameObject actor, Skill skill)
        {
            var actorData = actor.GetComponent<CharacterBattleData>();

            if (skill.Type == SkillTypes.Passion)
                return actorData.passion + skill.Power;
            else
            {
                return actorData.persuasion + skill.Power;
            }
        }

        public int CalculateJuryPoints(GameObject actor, Skill skill)
        {
            var actorData = actor.GetComponent<CharacterBattleData>();

            if (skill.Type == SkillTypes.Passion)
                return actorData.passion + skill.Power;
            else
            {
                return actorData.persuasion + skill.Power;
            }
        }

        public int CalculateJuryPointsFromPresentedEvidence(EvidenceEffectivenessTypes effectiveness) 
        {
            switch(effectiveness) 
            {
                case EvidenceEffectivenessTypes.Effective:
                return 20;

                case EvidenceEffectivenessTypes.Relevant:
                return 10;

                case EvidenceEffectivenessTypes.Ineffective:
                return 0;

                default:
                return 0;
            }
        }
    }
}
