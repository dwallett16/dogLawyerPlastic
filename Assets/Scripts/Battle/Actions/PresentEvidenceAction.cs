
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Battle.Actions;

public class PresentEvidenceAction : IAction
{
    public void Act(ActionData actionData)
    {
        var controller = ActionUtilities.Instance.GetBattleController();
        var currentCase = controller.battleData.CaseData;
        var prosecutors = controller.Prosecutors;

        var effectiveness = GetEffectiveness(currentCase.RelevantEvidence, currentCase.EffectiveEvidence, actionData.SelectedEvidence);

        var juryInfluencePoints = ActionUtilities.Instance.CalculateJuryPointsFromPresentedEvidence(effectiveness);
        actionData.Target.GetComponent<JuryController>().ChangePoints(juryInfluencePoints);

        var fpRestoration = ActionUtilities.Instance.CalculateFpRestorationFromPresentedEvidence(effectiveness);
        var spRestoration = ActionUtilities.Instance.CalculateSpRestorationFromPresentedEvidence(effectiveness);
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
            foreach (GameObject prosecutor in prosecutors)
            {
                prosecutor.GetComponent<CharacterBattleData>().AddStatusEffect(StatusEffects.Stunned, 3);
                Debug.Log("DA " + prosecutor.GetComponent<CharacterBattleData>().displayName + " now stunned");
            }
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
