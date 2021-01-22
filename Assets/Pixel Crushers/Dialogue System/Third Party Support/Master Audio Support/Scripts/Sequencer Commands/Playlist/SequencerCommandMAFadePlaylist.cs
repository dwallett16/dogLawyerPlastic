using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAFadePlaylist(targetVolume, fadeTime, [playlistControllerName])
	/// 
	/// - targetVolume: 0..1
	/// - fadeTime: 0..10
	/// - playlistControllerName: blank, 'all', or a playlist controller name
	/// </summary>
	public class SequencerCommandMAFadePlaylist : BaseMasterAudioSequencerCommand {

		public void Start() {
			float targetVolume = GetParameterAsFloat(0);
			float fadeTime = GetParameterAsFloat(1);
			string playlistControllerName = GetParameter(2);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAFadePlaylist({1}, {2}, {3})", new object[] {DialogueDebug.Prefix, targetVolume, fadeTime, playlistControllerName}));
			if (string.Equals(playlistControllerName, AllKeyword)) {
				var pcs = PlaylistController.Instances;
				for (var i = 0; i < pcs.Count; i++) {
					MasterAudio.FadePlaylistToVolume(pcs[i].name, targetVolume, fadeTime);
				}
			} else {
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.FadePlaylistToVolume(targetVolume, fadeTime);
				} else {
					MasterAudio.FadePlaylistToVolume(playlistControllerName, targetVolume, fadeTime);
				}
			}
			Stop();
		}
		
	}
	 
}
