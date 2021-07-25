using UnityEngine;

public class RestAction : IAction
{
    public void Act(ActionData actionData)
    {
        Debug.Log("Rest Action");
        Debug.Log("Previous FP - " + actionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints);

        actionData.CurrentCombatant.GetComponent<CharacterBattleData>().IncreaseFocusPoints(1);
        
        Debug.Log("Current FP - " + actionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints);
    }
}