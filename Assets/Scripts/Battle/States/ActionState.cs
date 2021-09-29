using Assets.Scripts.Battle.Actions;
using Assets.Scripts.Battle.Utilities;
using UnityEngine;

public class ActionState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        if(NewState) InitializeState("ActionState");

        controller.ActionData.Action.Act(controller.ActionData);

        controller.EndTurn.NewState = true;
        return controller.EndTurn;
    }
}
