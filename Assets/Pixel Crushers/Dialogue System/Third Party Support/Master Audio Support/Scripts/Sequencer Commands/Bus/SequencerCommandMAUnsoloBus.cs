using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAUnsoloBus(busName)
	/// 
	/// - busName: 'all' or a bus name
	/// </summary>
	public class SequencerCommandMAUnsoloBus : BaseMasterAudioSequencerCommand {

		public void Start() {
			string busName = GetParameter(0);
			if (string.IsNullOrEmpty(busName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAUnsoloBus({1}): busName is blank", new object[] {DialogueDebug.Prefix, busName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAUnsoloBus({1})", new object[] {DialogueDebug.Prefix, busName}));
				if (string.Equals(busName, AllKeyword)) {
					var busNames = MasterAudio.RuntimeBusNames;
					for (var i = 0; i < busNames.Count; i++) {
						MasterAudio.UnsoloBus(busNames[i]);
					}
				} else {
					MasterAudio.UnsoloBus(busName);
				}
			}
			Stop();
		}
		
	}
	 
}
