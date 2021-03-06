using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.Utilities
{
    public interface IActionUtilities
    {
        void SetAction(ActionData actionData);
        bool CalculateAttackSuccess(GameObject target);
        int CalculateJuryPoints(GameObject currentCombatant, Skill selectedSkill);
        bool CalculateDebuffSuccess(GameObject target);
        int CalculateStressAttackPower(GameObject actor, Skill skill);
        int CalculateStressRecoveryPower(GameObject actor, Skill skill);
        int CalculateJuryPointsFromPresentedEvidence(EvidenceEffectivenessType effectiveness);
        int CalculateSpRestorationFromPresentedEvidence(EvidenceEffectivenessType effectiveness);
        int CalculateFpRestorationFromPresentedEvidence(EvidenceEffectivenessType effectiveness);
        BattleController GetBattleController();
    }
}
