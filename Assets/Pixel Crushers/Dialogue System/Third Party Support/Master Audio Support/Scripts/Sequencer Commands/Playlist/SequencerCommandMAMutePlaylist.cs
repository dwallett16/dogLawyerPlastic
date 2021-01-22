using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAMutePlaylist([playlistControllerName])
	/// 
	/// - playlistControllerName: blank, 'all', or a playlist controller name
	/// </summary>
	public class SequencerCommandMAMutePlaylist : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string playlistControllerName = GetParameter(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAMutePlaylist({1})", new object[] {DialogueDebug.Prefix, playlistControllerName}));
			if (string.Equals(playlistControllerName, AllKeyword)) {
				MasterAudio.MuteAllPlaylists();
			} else {
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.MutePlaylist();
				} else {
					MasterAudio.MutePlaylist(playlistControllerName);
				}
			}
			Stop();
		}
		
	}
	
}
