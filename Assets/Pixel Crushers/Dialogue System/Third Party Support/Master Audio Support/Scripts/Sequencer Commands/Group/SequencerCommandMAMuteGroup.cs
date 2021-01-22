using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAMuteGroup(soundGroupName)
	/// 
	/// - soundGroupName: 'all' or a sound group name
	/// </summary>
	public class SequencerCommandMAMuteGroup : BaseMasterAudioSequencerCommand {

		public void Start() {
			string soundGroupName = GetParameter(0);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAMuteGroup({1}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAMuteGroup({1})", new object[] {DialogueDebug.Prefix, soundGroupName}));
				if (string.Equals(soundGroupName, AllKeyword)) {
					var groupNames = MasterAudio.RuntimeSoundGroupNames;
					for (var i = 0; i < groupNames.Count; i++) {
						MasterAudio.MuteGroup(groupNames[i]);
					}
				} else {
					MasterAudio.MuteGroup(soundGroupName);
				}
			}
			Stop();
		}
		
	}
	 
}
