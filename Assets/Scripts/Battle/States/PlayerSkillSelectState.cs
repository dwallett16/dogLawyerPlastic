using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSkillSelectState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        if (NewState)
        {
            InitializeState("PlayerSkillSelectState");
            controller.SkillPanel.SetActive(true);
            controller.ActionButtonPanel.SetActive(false);
            controller.TargetSelector.SetActive(false);
            for(int i = 0; i < controller.SkillButtons.Count; i++)
            {
                if (i < controller.ActionData.CurrentCombatantBattleData.skills.Count)
                {
                    if(i == 0)
                        EventSystem.current?.SetSelectedGameObject(controller.SkillButtons[i]);

                    controller.SkillButtons[i].SetActive(true);
                    controller.SkillButtons[i].GetComponentInChildren<Text>().text = controller.ActionData.CurrentCombatantBattleData.skills[i].Name;
                    controller.SkillButtons[i].GetComponent<SkillButtonData>().SkillData = controller.ActionData.CurrentCombatantBattleData.skills[i];
                }
                else
                {
                    controller.SkillButtons[i].SetActive(false);
                }
            }
        }
        else if(controller.IsBackButtonPressed) 
        {
            controller.PlayerActionSelect.NewState = true;
            return controller.PlayerActionSelect;
        }
        else if (controller.IsSubmitButtonPressed)
        {
            controller.PlayerTargetSelect.NewState = true;
            return controller.PlayerTargetSelect;
        }
        
        return this;
    }
}
