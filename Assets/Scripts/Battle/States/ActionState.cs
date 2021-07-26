using Assets.Scripts.Battle.Actions;
using UnityEngine;

public class ActionState : BattleState
{
    private readonly IActionUtilities actionUtilities;
    public ActionState()
    {
        actionUtilities = new ActionUtilities();
    }

    public override BattleState Execute(BattleController controller)
    {
        if(NewState) InitializeState("PlayerActionState");

        controller.ActionData.Action.Act(controller.ActionData);
        
        controller.AllCombatants.Enqueue(controller.ActionData.CurrentCombatant);

        controller.NextTurn.NewState = true;
        return controller.NextTurn;
    }
}
