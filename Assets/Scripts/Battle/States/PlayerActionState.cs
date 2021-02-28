using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionState : IBattleState
{
    public bool newState;
    public IBattleState Execute(BattleController controller)
    {
        if(newState) InitializeState(controller);

        if(controller.ActionData.ButtonAction == "Rest") {
            RestAction(controller);
        }

        return controller.NextTurn;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("Current State: PlayerActionSelectState");
        newState = false;
    }

    private void RestAction(BattleController controller) {
        Debug.Log("Rest Action");
        Debug.Log("Previous FP - " + controller.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints);
        controller.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints ++;
        Debug.Log("Current FP - " + controller.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints);
    }
}
