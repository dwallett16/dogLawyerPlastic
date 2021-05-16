using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle.Actions
{
    public interface IActionUtilities
    {
        void SetAction(ActionData actionData);
        bool CalculateAttackSuccess(GameObject target);
        int CalculateStressAttackPower(GameObject actor, Skill skill);
        int CalculateStressRecoveryPower(GameObject actor, Skill skill);
    }
}
