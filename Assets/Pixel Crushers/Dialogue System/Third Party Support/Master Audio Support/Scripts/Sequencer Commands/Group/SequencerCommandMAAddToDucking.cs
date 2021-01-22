using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAAddToDucking(soundGroupName, riseVolumeStart, [duckedVolMult], [unduckTime])
	/// 
	/// - soundGroupName: name of sound group
	/// - riseVolumeStart: percentage of sound played to start unducking
	/// - duckedVolMult: percentage of original volume (default: 0.5)
	/// - unduckTime: amount of time to return music to original volume (default: 1)
	/// </summary>
	public class SequencerCommandMAAddToDucking : BaseMasterAudioSequencerCommand {

		public void Start() {
			string soundGroupName = GetParameter(0);
			float riseVolumeStart = GetParameterAsFloat(1);
			float duckedVolMult = GetParameterAsFloat(2, 0.5f);
			float unduckTime = GetParameterAsFloat(3, 1);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAAddToDucking({1}, {2}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName, riseVolumeStart}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAAddToDucking({1}, {2})", new object[] {DialogueDebug.Prefix, soundGroupName, riseVolumeStart}));
				MasterAudio.AddSoundGroupToDuckList(soundGroupName, riseVolumeStart, duckedVolMult, unduckTime);
			}
			Stop();
		}
		
	}
	 
}
