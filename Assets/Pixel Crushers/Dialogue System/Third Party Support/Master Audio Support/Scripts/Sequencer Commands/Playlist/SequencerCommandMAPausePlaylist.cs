using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAPausePlaylist([playlistControllerName])
	/// 
	/// - playlistControllerName: blank, 'all', or a playlist controller name
	/// </summary>
	public class SequencerCommandMAPausePlaylist : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string playlistControllerName = GetParameter(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAPausePlaylist({1})", new object[] {DialogueDebug.Prefix, playlistControllerName}));
			if (string.Equals(playlistControllerName, AllKeyword)) {
				MasterAudio.PauseAllPlaylists();
			} else {
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.PausePlaylist();
				} else {
					MasterAudio.PausePlaylist(playlistControllerName);
				}
			}
			Stop();
		}
		
	}
	
}
