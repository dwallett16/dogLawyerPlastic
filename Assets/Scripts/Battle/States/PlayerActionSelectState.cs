using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerActionSelectState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        if (NewState)  {
            InitializeState("PlayerActionSelectState");
            if (controller.ActionData.CurrentCombatant != null) {
                Debug.Log("Current Combatant: " + controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().name);
            }
            controller.ActionButtonPanel.SetActive(true);
            EventSystem.current?.SetSelectedGameObject(controller.ActionButtonPanel.transform.GetChild(0).gameObject);
            controller.SkillPanel.SetActive(false);
            controller.EvidencePanel.SetActive(false);
            controller.ActionData.ButtonAction = string.Empty;
        }

        if(controller.ActionData.ButtonAction == Constants.Rest) {
            controller.ActionData.Action = new RestAction();
            
            controller.Action.NewState = true;
            return controller.Action;
        }

        if (controller.ActionData.ButtonAction == Constants.Skills)
        {
            controller.PlayerSkillSelect.NewState = true;
            return controller.PlayerSkillSelect;
        }

        if(controller.ActionData.ButtonAction == Constants.Evidence)
        {
            controller.PlayerEvidenceSelect.NewState = true;
            return controller.PlayerEvidenceSelect;
        }

        return this;
    }
}
