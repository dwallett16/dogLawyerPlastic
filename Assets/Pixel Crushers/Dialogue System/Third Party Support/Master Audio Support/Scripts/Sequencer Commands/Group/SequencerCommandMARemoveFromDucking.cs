using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MARemoveFromDucking(soundGroupName)
	/// 
	/// - soundGroupName: Name of sound group
	/// </summary>
	public class SequencerCommandMARemoveFromDucking : BaseMasterAudioSequencerCommand {

		public void Start() {
			string soundGroupName = GetParameter(0);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MARemoveFromDucking({1}): busName is blank", new object[] {DialogueDebug.Prefix, soundGroupName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MARemoveFromDucking({1})", new object[] {DialogueDebug.Prefix, soundGroupName}));
				MasterAudio.RemoveSoundGroupFromDuckList(soundGroupName);
			}
			Stop();
		}
		
	}
	 
}
