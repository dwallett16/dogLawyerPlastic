using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSelectState : IBattleState
{
    public bool newState;
    public IBattleState Execute(BattleController controller)
    {
        if (newState)  {
            InitializeState(controller);
            controller.CurrentCombatant = controller.AllCombatants.Dequeue();
        }

        if(controller.ActionData.ButtonAction == Constants.Rest) {
            controller.PlayerAction.newState = true;
            return controller.PlayerAction;
        }

        return this;
    }

    public void InitializeState(BattleController controller)
    {
        Debug.Log("Current State: PlayerActionSelectState");
        if (controller.CurrentCombatant != null)
        {
            Debug.Log("Current Combatant: " + controller.CurrentCombatant.GetComponent<CharacterBattleData>().name);
        }
        newState = false;
    }
}
