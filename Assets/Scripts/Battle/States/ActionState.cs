using UnityEngine;

public class ActionState : IBattleState
{
    public bool newState;
    public IBattleState Execute(BattleController controller)
    {
        if(newState) InitializeState(controller);

        controller.ActionData.Action.Act(controller.ActionData);
        
        controller.AllCombatants.Enqueue(controller.ActionData.CurrentCombatant);

        controller.NextTurn.newState = true;
        return controller.NextTurn;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("Current State: PlayerActionState");
        newState = false;
    }
}
