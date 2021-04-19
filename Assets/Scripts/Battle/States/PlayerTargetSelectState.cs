using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
                    controller.TargetList = controller.DefenseAttorneys;
                    controller.ActionData.Target = controller.TargetList[0];
                }
                else if (controller.ActionData.SelectedSkill.Target == SkillTarget.Prosecutors)
                {
                    controller.TargetList = controller.Prosecutors;
                    controller.ActionData.Target = controller.TargetList[0];
                }
                controller.TargetSelector.SetActive(true);
            }

            if (controller.IsBackButtonPressed)
            {
                controller.PlayerSkillSelect.NewState = true;
                return controller.PlayerSkillSelect;
            }
            int index = 0;
            if (controller.HorizontalAxis != 0)
            {
                index = controller.TargetList.FindIndex(x => x.name == controller.ActionData.Target.name);

                if (controller.HorizontalAxis < 0)
                {
                    if (index == 0) controller.ActionData.Target = controller.TargetList.Last();
                    else controller.ActionData.Target = controller.TargetList[index - 1];
                }
                else if (controller.HorizontalAxis > 0)
                {
                    if (index == controller.TargetList.Count - 1) controller.ActionData.Target = controller.TargetList.First();
                    else controller.ActionData.Target = controller.TargetList[index + 1];
                }
            }

            controller.TargetSelector.transform.position = new UnityEngine.Vector3(controller.ActionData.Target.transform.position.x, controller.TargetSelector.transform.position.y, controller.TargetSelector.transform.position.z);
            return this;
        }
    }
}
