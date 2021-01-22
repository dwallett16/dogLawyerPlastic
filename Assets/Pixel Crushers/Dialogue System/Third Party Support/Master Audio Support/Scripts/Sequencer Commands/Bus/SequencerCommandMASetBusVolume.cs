using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MASetBusVolume(busName, targetVolume)
	/// 
	/// - busName: 'all' or a bus name
	/// - targetVolume: 0..1
	/// </summary>
	public class SequencerCommandMASetBusVolume : BaseMasterAudioSequencerCommand {

		public void Start() {
			string busName = GetParameter(0);
			float targetVolume = GetParameterAsFloat(1);
			if (string.IsNullOrEmpty(busName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MASetBusVolume({1}, {2}): busName is blank", new object[] {DialogueDebug.Prefix, busName, targetVolume}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MASetBusVolume({1}, {2})", new object[] {DialogueDebug.Prefix, busName, targetVolume}));
				if (string.Equals(busName, AllKeyword)) {
					var busNames = MasterAudio.RuntimeBusNames;
					for (var i = 0; i < busNames.Count; i++) {
						MasterAudio.SetBusVolumeByName(busNames[i], targetVolume);
					}
				} else {
					MasterAudio.SetBusVolumeByName(busName, targetVolume);
				}
			}
			Stop();
		}
		
	}
	 
}
