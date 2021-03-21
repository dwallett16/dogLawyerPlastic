using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        InitializeState("InitialState");
        //TODO: Start with correct state depending on first combatant
        controller.PlayerActionSelect.NewState = true;
        return controller.PlayerActionSelect;
    }
}
