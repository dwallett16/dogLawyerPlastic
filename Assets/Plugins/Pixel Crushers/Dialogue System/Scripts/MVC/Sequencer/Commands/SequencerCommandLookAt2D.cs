// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Implements sequencer command: "LookAt(target, [, subject[, duration[, allAxes]]])", which rotates the 
    /// subject to face the target.
    /// 
    /// Arguments:
    /// -# Target to look at. Can be speaker, listener, or the name of a game object. Default: listener.
    /// -# (Optional) The subject; can be speaker, listener, or the name of a game object. Default: speaker.
    /// </summary>
    [AddComponentMenu("")] // Hide from menu.
    public class SequencerCommandLookAt2D : SequencerCommand
    {

        private const float SmoothMoveCutoff = 0.05f;

        private Transform target;
        private Transform subject;
        private float duration;
        float startTime;
        float endTime;
        Quaternion originalRotation;
        Quaternion targetRotation;

        public void Start()
        {
            // Get the values of the parameters:
            target = GetSubject(0, sequencer.listener);
            subject = GetSubject(1);

            if (DialogueDebug.logInfo) Debug.Log(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}: Sequencer: LookAt({1}, {2}, {3})", new System.Object[] { DialogueDebug.Prefix, target, subject, duration }));
            if ((target == null) && DialogueDebug.logWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: LookAt Target '{1}' wasn't found.", new System.Object[] { DialogueDebug.Prefix, GetParameter(0) }));
            if ((subject == null) && DialogueDebug.logWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: LookAt Subject '{1}' wasn't found.", new System.Object[] { DialogueDebug.Prefix, GetParameter(1) }));

            if ((subject != null) && (target != null) && (subject != target))
            {
                var scale = subject.transform.localScale;
                var difference = subject.position - target.position;
                if (subject.name == Constants.PlayerTag)
                {
                    if (difference.x < 0)
                    {
                        scale.x = scale.x < 0 ? scale.x * -1f : scale.x;
                    }
                    else
                    {
                        scale.x = scale.x >= 0 ? scale.x * -1f : scale.x;
                    }
                }
                else
                {
                    var facingPoint = GameObject.Find(DialogueLua.GetVariable("Conversant").AsString + "FacingPoint");
                    var isFacingRight = facingPoint.transform.position.x - subject.position.x > 0;

                    if ((!isFacingRight && difference.x < 0) || (isFacingRight && difference.x > 0))
                    {
                        scale.x = scale.x * -1f;
                    }
                    
                }
                    
                subject.transform.localScale = scale;
            }
            Stop();
        }
    }

}
