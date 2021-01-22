using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MASetMasterVolume(volume)
	/// 
	/// - volume: 0..1
	/// </summary>
	public class SequencerCommandMASetMasterVolume : BaseMasterAudioSequencerCommand {

		public void Start() {
			float volume = GetParameterAsFloat(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MASetMasterVolume({1})", new object[] {DialogueDebug.Prefix, volume}));
			MasterAudio.MasterVolumeLevel = volume;
			Stop();
		}
		
	}
	 
}
