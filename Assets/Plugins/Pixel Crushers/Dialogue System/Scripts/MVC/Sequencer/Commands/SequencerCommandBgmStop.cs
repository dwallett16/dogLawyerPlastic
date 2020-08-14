using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandBgmStop : SequencerCommand
    {

        public void Awake()
        {
            SoundManager.Instance.BgmStop();
            Stop();
        }
    }

}

