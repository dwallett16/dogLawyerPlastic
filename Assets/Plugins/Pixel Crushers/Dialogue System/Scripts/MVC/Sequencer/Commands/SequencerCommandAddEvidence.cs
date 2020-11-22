using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandAddEvidence : SequencerCommand
    {

        public void Start()
        {
            var evidenceId = GetParameter(0);
            if(SequencerObserverContainer.Instance.AddEvidenceObserver == null)
                SequencerObserverContainer.Instance.AddEvidenceObserver = new AddEvidenceObserver();
            
            SequencerObserverContainer.Instance.AddEvidenceObserver.UpdateData(evidenceId);
            Stop();
        }
    }

}

