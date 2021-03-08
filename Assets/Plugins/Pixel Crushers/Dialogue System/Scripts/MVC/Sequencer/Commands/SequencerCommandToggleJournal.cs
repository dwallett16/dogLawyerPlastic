using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandToggleJournal : SequencerCommand
    {

        public void Start()
        {
            var subject = GetSubject(1);
            var listener = subject.GetComponent<IJournalEventListener>();
            listener.ToggleJournal();
            Stop();
        }
    }
}

