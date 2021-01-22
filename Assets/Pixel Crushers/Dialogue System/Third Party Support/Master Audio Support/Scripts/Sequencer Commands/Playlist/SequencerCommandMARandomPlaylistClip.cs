using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MARandomPlaylistClip([playlistControllerName])
	/// 
	/// - playlistControllerName: blank, 'all', or a playlist controller name
	/// </summary>
	public class SequencerCommandMARandomPlaylistClip : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string playlistControllerName = GetParameter(0);
			if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MARandomPlaylistClip({1})", new object[] {DialogueDebug.Prefix, playlistControllerName}));
			if (string.Equals(playlistControllerName, AllKeyword)) {
				var pcs = PlaylistController.Instances;
				for (var i = 0; i < pcs.Count; i++) {
					MasterAudio.TriggerRandomPlaylistClip(pcs[i].name);
				}
			} else {
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.TriggerRandomPlaylistClip();
				} else {
					MasterAudio.TriggerRandomPlaylistClip(playlistControllerName);
				}
			}
			Stop();
		}
		
	}
	
}
