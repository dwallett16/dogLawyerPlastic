using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSelectState : IBattleState
{
    public bool newState;
    public IBattleState Execute(BattleController controller)
    {
        if (newState) InitializeState(controller);

        if(controller.ActionData.ButtonAction == "Rest") {
            controller.PlayerAction.newState = true;
            return controller.PlayerAction;
        }

        return this;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("Current State: PlayerActionSelectState");
        var currentCombatantBattleData = controller.CurrentCombatant.GetComponent<CharacterBattleData>();
        if (currentCombatantBattleData != null)
        {
            Debug.Log("Current Combatant: " + currentCombatantBattleData.name);
        }
        newState = false;
    }
}
