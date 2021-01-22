using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Sequencer command MAFireCustomEvent(customEventName[, origin])
    /// 
    /// - customEventName: name of custom event defined in Master Audio prefab
    /// </summary>
    public class SequencerCommandMAFireCustomEvent : BaseMasterAudioSequencerCommand
    {

        public void Start()
        {
            string customEventName = GetParameter(0);
            Transform origin = GetSubject(1) ?? GetSubject(GetParameter(1) + "(Clone)");
            if (string.IsNullOrEmpty(customEventName))
            {
                if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAFireCustomEvent({1}, {2}): customEventName is blank", new object[] { DialogueDebug.Prefix, customEventName, GetParameter(1) }));
            }
            else
            {
                if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAFireCustomEvent({1}, {2})", new object[] { DialogueDebug.Prefix, customEventName, GetParameter(1) }));
                MasterAudio.FireCustomEvent(customEventName, origin);
            }
            Stop();
        }

    }

}
