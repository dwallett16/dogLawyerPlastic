using Assets.Scripts.Battle.Actions;
using Assets.Scripts.Battle.Utilities;
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
        if(NewState) InitializeState("ActionState");

        controller.ActionData.Action.Act(controller.ActionData);

        return controller.EndTurn;
    }
}
