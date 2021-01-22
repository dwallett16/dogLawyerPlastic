using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAStopTransformSound(subject, soundGroupName)
	/// 
	/// - subject: 'speaker', 'listener' or object name. Default: speaker.
	/// - soundGroupName: 'all' or a sound group name
	/// </summary>
	public class SequencerCommandMAStopTransformSound : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			Transform subject = GetSubject (0, Sequencer.Speaker);
			string soundGroupName = GetParameter(1);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAStopTransformSound({1}, {2}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, GetParameter(1), soundGroupName}));
			} else if (subject == null) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAStopTransformSound({1}, {2}): subject is null", new object[] {DialogueDebug.Prefix, GetParameter(1), soundGroupName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAStopTransformSound({1}, {2})", new object[] {DialogueDebug.Prefix, subject.name, soundGroupName}));
				if (string.Equals(soundGroupName, AllKeyword)) {
					MasterAudio.StopAllSoundsOfTransform(subject);
				} else {
					MasterAudio.StopSoundGroupOfTransform(subject, soundGroupName);
				}
			}
			Stop();
		}
		
	}
	
}
