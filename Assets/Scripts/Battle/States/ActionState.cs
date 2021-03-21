using UnityEngine;

public class ActionState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        if(NewState) InitializeState("PlayerActionState");

        controller.ActionData.Action.Act(controller.ActionData);
        
        controller.AllCombatants.Enqueue(controller.ActionData.CurrentCombatant);

        controller.NextTurn.NewState = true;
        return controller.NextTurn;
    }
}
