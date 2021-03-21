using UnityEngine;

public abstract class BattleState
{
    public bool NewState;
    public abstract BattleState Execute(BattleController controller);
    public virtual void InitializeState(string stateName) {
        NewState = false;
        Debug.Log("Current State: " + stateName);
    }
}
