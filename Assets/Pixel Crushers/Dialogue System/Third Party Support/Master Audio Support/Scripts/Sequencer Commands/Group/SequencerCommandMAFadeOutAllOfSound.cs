using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAFadeOutAllOfSound(soundGroupName, fadeTime)
	/// 
	/// - soundGroupName: 'all' or a sound group name
	/// - fadeTime: 0..10
	/// </summary>
	public class SequencerCommandMAFadeOutAllOfSound : BaseMasterAudioSequencerCommand {

		public void Start() {
			string soundGroupName = GetParameter(0);
			float fadeTime = GetParameterAsFloat (1);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAFadeOutAllOfSound({1}, {2}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName, fadeTime}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAFadeOutAllOfSound({1}, {2})", new object[] {DialogueDebug.Prefix, soundGroupName, fadeTime}));
				if (string.Equals(soundGroupName, AllKeyword)) {
					var groupNames = MasterAudio.RuntimeSoundGroupNames;
					for (var i = 0; i < groupNames.Count; i++) {
						MasterAudio.FadeOutAllOfSound(groupNames[i], fadeTime);
					}
				} else {
					MasterAudio.FadeOutAllOfSound(soundGroupName, fadeTime);
				}
			}
			Stop();
		}
		
	}
	 
}
