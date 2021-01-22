using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAFadeBus(busName, targetVolume, fadeTime)
	/// 
	/// - busName: 'all' or a bus name
	/// - targetVolume: 0..1
	/// - fadeTime: 0..10
	/// </summary>
	public class SequencerCommandMAFadeBus : BaseMasterAudioSequencerCommand {

		public void Start() {
			string busName = GetParameter(0);
			float targetVolume = GetParameterAsFloat(1);
			float fadeTime = GetParameterAsFloat(2);
			if (string.IsNullOrEmpty(busName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAFadeBus({1}, {2}, {3}): busName is blank", new object[] {DialogueDebug.Prefix, busName, targetVolume, fadeTime}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAFadeBus({1}, {2}, {3})", new object[] {DialogueDebug.Prefix, busName, targetVolume, fadeTime}));
				if (string.Equals(busName, AllKeyword)) {
					var busNames = MasterAudio.RuntimeBusNames;
					for (var i = 0; i < busNames.Count; i++) {
						MasterAudio.FadeBusToVolume(busNames[i], targetVolume, fadeTime);
					}
				} else {
					MasterAudio.FadeBusToVolume(busName, targetVolume, fadeTime);
				}
			}
			Stop();
		}
		
	}
	 
}
