using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAChangePlaylist(playlistName, [playlistControllerName])
	/// 
	/// - playlistName: playlist to change to
	/// - playlistControllerName: (optional)
	/// </summary>
	public class SequencerCommandMAChangePlaylist : BaseMasterAudioSequencerCommand {

		public void Start() {
			string playlistName = GetParameter(0);
			string playlistControllerName = GetParameter (1);
			if (string.IsNullOrEmpty(playlistName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAChangePlaylist({1}, {2}): playlistName is blank", new object[] {DialogueDebug.Prefix, playlistName, playlistControllerName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAChangePlaylist({1}, {2})", new object[] {DialogueDebug.Prefix, playlistName, playlistControllerName}));
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.ChangePlaylistByName(playlistName, true);
				} else {
					MasterAudio.ChangePlaylistByName(playlistControllerName, playlistName, true);
				}
			}
			Stop();
		}
		
	}
	 
}
