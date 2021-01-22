using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAPlaylistClip(clip, [playlistControllerName])
	/// 
	/// - clipName: clip to play
	/// - playlistControllerName: (optional)
	/// </summary>
	public class SequencerCommandMAPlaylistClip : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string clipName = GetParameter(0);
			string playlistControllerName = GetParameter (1);
			if (string.IsNullOrEmpty(clipName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAPlaylistClip({1}, {2}): clipName is blank", new object[] {DialogueDebug.Prefix, clipName, playlistControllerName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAPlaylistClip({1}, {2})", new object[] {DialogueDebug.Prefix, clipName, playlistControllerName}));
				if (string.IsNullOrEmpty(playlistControllerName)) {
					MasterAudio.TriggerPlaylistClip(clipName);
				} else {
					MasterAudio.TriggerPlaylistClip(playlistControllerName, clipName);
				}
			}
			Stop();
		}
		
	}
	
}
