using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAUnmutePlaylist([playlistControllerName])
	/// 
	/// - playlistControllerName: blank, 'all', or a playlist controller name
	/// </summary>
	public class SequencerCommandMAUnmutePlaylist : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string playlistControllerName = GetParameter(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAUnmutePlaylist({1})", new object[] {DialogueDebug.Prefix, playlistControllerName}));
			if (string.Equals(playlistControllerName, AllKeyword)) {
				MasterAudio.UnmuteAllPlaylists();
			} else {
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.UnmutePlaylist();
				} else {
					MasterAudio.UnmutePlaylist(playlistControllerName);
				}
			}
			Stop();
		}
		
	}
	
}
