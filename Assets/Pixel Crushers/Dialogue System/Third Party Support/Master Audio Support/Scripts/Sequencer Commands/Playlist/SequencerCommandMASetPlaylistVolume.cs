using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MASetPlaylistVolume(volume)
	/// 
	/// - volume: 0..1
	/// </summary>
	public class SequencerCommandMASetPlaylistVolume : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			float volume = GetParameterAsFloat(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MASetPlaylistVolume({1})", new object[] {DialogueDebug.Prefix, volume}));
			MasterAudio.PlaylistMasterVolume = volume;
			Stop();
		}
		
	}
	
}
