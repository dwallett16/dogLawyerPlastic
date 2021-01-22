using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAStopPlaylist([playlistControllerName])
	/// 
	/// - playlistControllerName: blank, 'all', or a playlist controller name
	/// </summary>
	public class SequencerCommandMAStopPlaylist : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string playlistControllerName = GetParameter(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAStopPlaylist({1})", new object[] {DialogueDebug.Prefix, playlistControllerName}));
			if (string.Equals(playlistControllerName, AllKeyword)) {
				MasterAudio.StopAllPlaylists();
			} else {
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.StopPlaylist();
				} else {
					MasterAudio.StopPlaylist(playlistControllerName);
				}
			}
			Stop();
		}
		
	}
	
}
