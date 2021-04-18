using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.States
{
    public class PlayerTargetSelectState : BattleState
    {
        public override BattleState Execute(BattleController controller)
        {
            if (NewState)
            {
                InitializeState("PlayerTargetSelectState");
                controller.SkillPanel.SetActive(false);
                if (controller.ActionData.SelectedSkill.Target == SkillTarget.DefenseAttorneys)
                {
                    controller.ActionData.Target = controller.DefenseAttorneys[0];
                }
                controller.TargetSelector.SetActive(true);
            }
            controller.TargetSelector.transform.position = new UnityEngine.Vector3(controller.ActionData.Target.transform.position.x, controller.TargetSelector.transform.position.y, controller.TargetSelector.transform.position.z);
            return this;
        }
    }
}
