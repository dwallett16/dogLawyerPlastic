using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAFadeGroup(soundGroupName, targetVolume, fadeTime)
	/// 
	/// - soundGroupName: 'all' or a sound group name
	/// - targetVolume: 0..1
	/// - fadeTime: 0..10
	/// </summary>
	public class SequencerCommandMAFadeGroup : BaseMasterAudioSequencerCommand {

		public void Start() {
			string soundGroupName = GetParameter(0);
			float targetVolume = GetParameterAsFloat(1);
			float fadeTime = GetParameterAsFloat(2);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAFadeGroup({1}, {2}, {3}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName, targetVolume, fadeTime}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAFadeGroup({1}, {2}, {3})", new object[] {DialogueDebug.Prefix, soundGroupName, targetVolume, fadeTime}));
				if (string.Equals(soundGroupName, AllKeyword)) {
					var groupNames = MasterAudio.RuntimeSoundGroupNames;
					for (var i = 0; i < groupNames.Count; i++) {
						MasterAudio.FadeSoundGroupToVolume(groupNames[i], targetVolume, fadeTime);
					}
				} else {
					MasterAudio.FadeSoundGroupToVolume(soundGroupName, targetVolume, fadeTime);
				}
			}
			Stop();
		}
		
	}
	 
}
