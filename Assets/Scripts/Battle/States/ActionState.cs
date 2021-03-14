using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : IBattleState
{
    public bool newState;
    public IBattleState Execute(BattleController controller)
    {
        if(newState) InitializeState(controller);

        if(controller.ActionData.ButtonAction == Constants.Rest) {
            RestAction(controller);
        }
        controller.AllCombatants.Enqueue(controller.ActionData.CurrentCombatant);
        return controller.NextTurn;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("Current State: PlayerActionState");
        newState = false;
    }

    private void RestAction(BattleController controller) {
        Debug.Log("Rest Action");
        Debug.Log("Previous FP - " + controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints);
        controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints ++;
        Debug.Log("Current FP - " + controller.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints);
    }
}
