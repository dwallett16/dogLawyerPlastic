
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PresentEvidenceAction : IAction
{
    public void Act(ActionData actionData)
    {
        var effectiveness = GetEffectiveness(actionData.CurrentCase.RelevantEvidence, actionData.CurrentCase.EffectiveEvidence, actionData.SelectedEvidence);
        var juryInfluencePoints = actionData.ActionUtilities.CalculateJuryPointsFromPresentedEvidence(effectiveness);
        actionData.Jury.GetComponent<JuryController>().ChangePoints(juryInfluencePoints);
        Debug.Log("Presenting" + effectiveness.ToString() + "evidence");
        
        //need to remove evidence from inventory
    }

    private EvidenceEffectivenessTypes GetEffectiveness(List<Evidence> relevantEvidence, List<Evidence> effectiveEvidence, Evidence selectedEvidence) 
    {
        if (relevantEvidence.Any(x => x.Id == selectedEvidence.Id))
        {
            return EvidenceEffectivenessTypes.Relevant;
        }  
        else if (effectiveEvidence.Any(x => x.Id == selectedEvidence.Id))
        {
            return EvidenceEffectivenessTypes.Effective;
        }
        else
        {
            return EvidenceEffectivenessTypes.Ineffective;
        }
    }
}
