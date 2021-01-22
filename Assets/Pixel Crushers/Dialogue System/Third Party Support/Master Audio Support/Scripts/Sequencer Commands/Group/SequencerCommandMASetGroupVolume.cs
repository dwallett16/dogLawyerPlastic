using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MASetGroupVolume(soundGroupName, volume)
	/// 
	/// - soundGroupName: 'all' or a sound group name
	/// - volume: 0..1
	/// </summary>
	public class SequencerCommandMASetGroupVolume : BaseMasterAudioSequencerCommand {

		public void Start() {
			string soundGroupName = GetParameter(0);
			float volume = GetParameterAsFloat(1);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MASetGroupVolume({1}, {2}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName, volume}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MASetGroupVolume({1}, {2})", new object[] {DialogueDebug.Prefix, soundGroupName, volume}));
				if (string.Equals(soundGroupName, AllKeyword)) {
					var groupNames = MasterAudio.RuntimeSoundGroupNames;
					for (var i = 0; i < groupNames.Count; i++) {
						MasterAudio.SetGroupVolume(groupNames[i], volume);
					}
				} else {
					MasterAudio.SetGroupVolume(soundGroupName, volume);
				}
			}
			Stop();
		}
		
	}
	 
}
