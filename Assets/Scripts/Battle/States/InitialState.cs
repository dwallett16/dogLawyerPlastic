using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IBattleState
{
    public IBattleState Execute(BattleController controller)
    {
        InitializeState(controller);
        
        controller.PlayerActionSelect.newState = true;
        return controller.PlayerActionSelect;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("CurrentState: InitialState");
    }
}
