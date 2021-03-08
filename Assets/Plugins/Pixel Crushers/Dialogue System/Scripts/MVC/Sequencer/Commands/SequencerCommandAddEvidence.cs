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
            var subject = GetSubject(1);
            var listener = subject.GetComponent<IInventoryListener>();
            listener.NotifyEvidence(evidenceId);
            Stop();
        }
    }
}

