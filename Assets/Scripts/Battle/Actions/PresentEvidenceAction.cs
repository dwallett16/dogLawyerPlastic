
using UnityEngine;
using System.Linq;

public class PresentEvidenceAction : IAction
{
    public void Act(ActionData actionData)
    {
        if (actionData.CurrentCase.RelevantEvidence.Any(x => x.Id == actionData.SelectedEvidence.Id))
        {
            Debug.Log("Presenting relevant evidence");
            actionData.Jury.GetComponent<JuryController>().ChangePoints(10);
        }  
        else if (actionData.CurrentCase.EffectiveEvidence.Any(x => x.Id == actionData.SelectedEvidence.Id))
        {
            Debug.Log("Presenting effective evidence");
            actionData.Jury.GetComponent<JuryController>().ChangePoints(20);
        }
        else
        {
            Debug.Log("Presenting ineffective evidence");
        }
        //use enums and put logic in utilities
    }
}
