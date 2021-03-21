using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSelectState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        if (NewState)
        {
            for(int i = 0; i < controller.SkillButtons.Count; i++)
            {
                if (i < controller.ActionData.CurrentCombatantBattleData.skills.Count)
                {
                    controller.SkillButtons[i].SetActive(true);
                    controller.SkillButtons[i].GetComponentInChildren<Text>().text = controller.ActionData.CurrentCombatantBattleData.skills[i].name;
                }
                else
                {
                    controller.SkillButtons[i].SetActive(false);
                }
            }
        }
        return this;
    }
}
