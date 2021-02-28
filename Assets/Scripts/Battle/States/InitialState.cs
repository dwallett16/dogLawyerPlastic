using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IBattleState
{
    public IBattleState Execute(BattleController controller)
    {
        throw new System.NotImplementedException();
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("CurrentState: InitialState");
    }
}
