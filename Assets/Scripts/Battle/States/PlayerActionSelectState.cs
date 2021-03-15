using UnityEngine;

public class PlayerActionSelectState : IBattleState
{
    public bool newState;
    public IBattleState Execute(BattleController controller)
    {
        if (newState)  {
            InitializeState(controller);
            controller.ActionData.CurrentCombatant = controller.AllCombatants.Dequeue();
        }

        if(controller.ActionData.ButtonAction == Constants.Rest) {
            controller.ActionData.Action = new RestAction();
            controller.Action.newState = true;
            return controller.Action;
        }

        return this;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("Current State: PlayerActionSelectState");
        if (controller.ActionData.CurrentCombatant != null)
        {
            Debug.Log("Current Combatant: " + controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().name);
        }
        newState = false;
    }
}
