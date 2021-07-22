
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

        var fpRestoration = actionData.ActionUtilities.CalculateFpRestorationFromPresentedEvidence(effectiveness);
        var spRestoration = actionData.ActionUtilities.CalculateSpRestorationFromPresentedEvidence(effectiveness);
        foreach(GameObject prosecutor in actionData.Prosecutors)
        {
            var data = prosecutor.GetComponent<CharacterBattleData>();
            data.currentFocusPoints += fpRestoration;
            data.currentStress -= spRestoration;
        }

        Debug.Log("Presenting " + effectiveness.ToString() + " evidence. Added " + juryInfluencePoints + " jury points. Restored " + 
            fpRestoration + " focus points and " + spRestoration + " stress points to all prosecutors.");
    }

    private EvidenceEffectivenessType GetEffectiveness(List<Evidence> relevantEvidence, List<Evidence> effectiveEvidence, Evidence selectedEvidence) 
    {
        if (relevantEvidence.Any(x => x.Id == selectedEvidence.Id))
        {
            return EvidenceEffectivenessType.Relevant;
        }  
        else if (effectiveEvidence.Any(x => x.Id == selectedEvidence.Id))
        {
            return EvidenceEffectivenessType.Effective;
        }
        else
        {
            return EvidenceEffectivenessType.Ineffective;
        }
    }
}
