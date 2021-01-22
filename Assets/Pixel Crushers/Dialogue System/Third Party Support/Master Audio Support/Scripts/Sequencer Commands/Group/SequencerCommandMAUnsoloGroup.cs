using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAUnsoloGroup(soundGroupName)
	/// 
	/// - soundGroupName: 'all' or a sound group name
	/// </summary>
	public class SequencerCommandMAUnsoloGroup : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string soundGroupName = GetParameter(0);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAUnsoloGroup({1}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAUnsoloGroup({1})", new object[] {DialogueDebug.Prefix, soundGroupName}));
				if (string.Equals(soundGroupName, AllKeyword)) {
					var groupNames = MasterAudio.RuntimeSoundGroupNames;
					for (var i = 0; i < groupNames.Count; i++) {
						MasterAudio.UnsoloGroup(groupNames[i]);
					}
				} else {
					MasterAudio.UnsoloGroup(soundGroupName);
				}
			}
			Stop();
		}
		
	}
	
}
