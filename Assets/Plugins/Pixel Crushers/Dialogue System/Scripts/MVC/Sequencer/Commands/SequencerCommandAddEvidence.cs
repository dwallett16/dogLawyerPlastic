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
            SequencerObserver.Instance.UpdateData(evidenceId);
            Stop();
        }
    }

}

