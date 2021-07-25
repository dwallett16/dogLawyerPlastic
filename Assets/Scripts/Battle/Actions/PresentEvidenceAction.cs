
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PresentEvidenceAction : IAction
{
    public void Act(ActionData actionData)
    {
        var controller = GameObject.Find("BattleController").GetComponent<BattleController>();
        var currentCase = controller.battleData.CaseData;
        var prosecutors = controller.Prosecutors;

        var effectiveness = GetEffectiveness(currentCase.RelevantEvidence, currentCase.EffectiveEvidence, actionData.SelectedEvidence);

        var juryInfluencePoints = actionData.ActionUtilities.CalculateJuryPointsFromPresentedEvidence(effectiveness);
        actionData.Target.GetComponent<JuryController>().ChangePoints(juryInfluencePoints);

        var fpRestoration = actionData.ActionUtilities.CalculateFpRestorationFromPresentedEvidence(effectiveness);
        var spRestoration = actionData.ActionUtilities.CalculateSpRestorationFromPresentedEvidence(effectiveness);
        foreach(GameObject prosecutor in prosecutors)
        {
            var data = prosecutor.GetComponent<CharacterBattleData>();
            data.IncreaseFocusPoints(fpRestoration);
            data.ReduceStress(spRestoration);
        }
        
        if (effectiveness == EvidenceEffectivenessType.Effective)
            controller.EffectiveEvidenceCount++;

        if (controller.EffectiveEvidenceCount == 3)
        {

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
