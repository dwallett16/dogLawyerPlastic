// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Implements sequencer command: "MoveTo(target, [, subject[, duration]])", which matches the 
    /// subject to the target's position and rotation. If the subject has a rigidbody, uses
    /// Rigidbody.MovePosition/Rotation; otherwise sets the transform directly.
    /// 
    /// Arguments:
    /// -# The target. 
    /// -# (Optional) The subject; can be speaker, listener, or the name of a game object. 
    /// Default: speaker.
    /// -# (Optional) Duration in seconds.
    /// </summary>
    [AddComponentMenu("")] // Hide from menu.
    public class SequencerCommandMoveToCharacter : SequencerCommand
    {

        private const float SmoothMoveCutoff = 0.05f;

        private Transform target;
        private Transform subject;
        private Rigidbody subjectRigidbody;
        private float duration;
        private bool onlyXAxis;
        float startTime;
        float endTime;
        Vector3 originalPosition;
        Quaternion originalRotation;
        float originalZ;
        float originalY;

        public void Start()
        {
            // Get the values of the parameters:
            target = GetSubject(0);
            subject = GetSubject(1);
            duration = GetParameterAsFloat(2, 0);
            onlyXAxis = GetParameterAsBool(3);
            if (DialogueDebug.logInfo) Debug.Log(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}: Sequencer: MoveTo({1}, {2}, {3})", new System.Object[] { DialogueDebug.Prefix, target, subject, duration }));
            if ((target == null) && DialogueDebug.logWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MoveTo() target '{1}' wasn't found.", new System.Object[] { DialogueDebug.Prefix, GetParameter(0) }));
            if ((subject == null) && DialogueDebug.logWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MoveTo() subject '{1}' wasn't found.", new System.Object[] { DialogueDebug.Prefix, GetParameter(1) }));

            // Set up the move:
            if ((subject != null) && (target != null) && (subject != target))
            {
                subjectRigidbody = subject.GetComponent<Rigidbody>();
                var leftPoint = GameObject.Find(DialogueLua.GetVariable("Conversant").AsString + "LeftPoint");
                var rightPoint = GameObject.Find(DialogueLua.GetVariable("Conversant").AsString + "RightPoint");
                var leftDistance = Vector2.Distance(subject.position, leftPoint.transform.position);
                var rightDistance = Vector2.Distance(subject.position, rightPoint.transform.position);
                target = leftDistance <= rightDistance ? leftPoint.transform : rightPoint.transform;
                
                if (duration > SmoothMoveCutoff)
                {
                    startTime = DialogueTime.time;
                    endTime = startTime + duration;
                    originalPosition = subject.position;
                    originalRotation = subject.rotation;
                    originalZ = subject.position.z;
                    originalY = subject.position.y;
                }
                else
                {
                    Stop();
                }
            }
            else
            {
                Stop();
            }
        }

        private void SetPosition(Vector3 newPosition, Quaternion newRotation)
        {
            // For efficiency, doesn't warp NavMeshAgent.
            if (subjectRigidbody != null && !subjectRigidbody.isKinematic)
            {
                //subjectRigidbody.MoveRotation(newRotation);
                subjectRigidbody.MovePosition(newPosition);
            }
            else
            {
                //subject.rotation = newRotation;
                subject.position = newPosition;
            }
        }

        public void Update()
        {
            // Keep smoothing for the specified duration:
            if (DialogueTime.time < endTime)
            {
                float elapsed = (DialogueTime.time - startTime) / duration;
                var moveToPos = Vector3.Lerp(originalPosition, target.position, elapsed);
                moveToPos.z = onlyXAxis ? originalZ : moveToPos.z;
                moveToPos.y = onlyXAxis ? originalY : moveToPos.y;
                SetPosition(moveToPos, Quaternion.Lerp(originalRotation, target.rotation, elapsed));
            }
            else
            {
                Stop();
            }
        }

        public void OnDestroy()
        {
            // Final position:
            if ((subject != null) && (target != null) && (subject != target))
            {
                var moveToPos = new Vector3(target.position.x, target.position.y, target.position.z);
                moveToPos.z = onlyXAxis ? originalZ : moveToPos.z;
                moveToPos.y = onlyXAxis ? originalY : moveToPos.y;
                SetPosition(moveToPos, target.rotation);
            }

        }

    }

}
