using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : BattleState
{
    public override BattleState Execute(BattleController controller)
    {
        InitializeState("InitialState");
        return controller.NextTurn;
    }
}
