using UnityEngine;

public class PlayerActionSelectState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        if (NewState)  {
            InitializeState("PlayerActionSelectState");
            if (controller.ActionData.CurrentCombatant != null) {
                Debug.Log("Current Combatant: " + controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().name);
            }

            controller.ActionData.CurrentCombatant = controller.AllCombatants.Dequeue();
            controller.ActionData.CurrentCombatantBattleData = controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>();
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

        return this;
    }
}
