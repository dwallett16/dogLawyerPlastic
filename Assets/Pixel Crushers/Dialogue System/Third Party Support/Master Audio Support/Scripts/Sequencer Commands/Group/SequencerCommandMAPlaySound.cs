using UnityEngine;
using System.Collections;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Sequencer command MAPlaySound(group, variation, volume, pitch, wait, subject, follow)
    /// 
    /// - group: The name of the Sound Group to trigger a sound from.
    /// - variation: Play a specific variation by name. If blank, a random variation is played.
    /// - volume: Volume percentage (0-1). Default: 1.
    /// - pitch: Pitch adjustment. Leave blank for no pitch change.
    /// - wait: Specify true to wait until the sound is done playing. Default: false.
    /// - subject: GameObject whose position you want the sound to eminate from. Leave blank to play the sound 2D.
    /// - follow: Specify true to follow the subject. Leave blank to not follow.
    /// </summary>
    public class SequencerCommandMAPlaySound : BaseMasterAudioSequencerCommand
    {

        public IEnumerator Start()
        {
            string groupName = GetParameter(0);
            string variationName = GetParameter(1);
            float volume = GetParameterAsFloat(2, 1);
            bool usePitch = !string.IsNullOrEmpty(GetParameter(3));
            float? pitch = null;
            if (usePitch) pitch = GetParameterAsFloat(3, 1);
            bool wait = GetParameterAsBool(4);
            Transform subject = GetSubject(5);
            bool follow = GetParameterAsBool(6);
            if (string.IsNullOrEmpty(groupName))
            {
                if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAPlaySound({1}): group name is blank", new object[] { DialogueDebug.Prefix, GetParameters() }));
            }
            else
            {
                if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAPlaySound(group={1}, variation={2}, volume={3}, pitch={4}, wait={5}, subject={6}, follow={7})", new object[] { DialogueDebug.Prefix, groupName, variationName, volume, pitch, wait, subject, follow }));

                if (subject == null) // 2D sound:
                {
                    if (wait)
                    {
                        yield return StartCoroutine(MasterAudio.PlaySoundAndWaitUntilFinished(groupName, volume, pitch, 0, variationName));
                    }
                    else
                    {
                        MasterAudio.PlaySoundAndForget(groupName, volume, pitch, 0, variationName);
                    }
                }
                else // 3D sound:
                {
                    if (wait)
                    {
                        if (follow)
                        {
                            yield return StartCoroutine(MasterAudio.PlaySound3DFollowTransformAndWaitUntilFinished(groupName, subject, volume, pitch, 0, variationName));
                        }
                        else
                        {
                            yield return StartCoroutine(MasterAudio.PlaySound3DAtTransformAndWaitUntilFinished(groupName, subject, volume, pitch, 0, variationName));
                        }
                    }
                    else
                    {
                        if (follow)
                        {
                            MasterAudio.PlaySound3DFollowTransformAndForget(groupName, subject, volume, pitch, 0, variationName);
                        }
                        else
                        {
                            MasterAudio.PlaySound3DAtTransformAndForget(groupName, subject, volume, pitch, 0, variationName);
                        }
                    }
                }
            }
            Stop();
        }

    }

}
