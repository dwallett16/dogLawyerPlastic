using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAUnpauseBus(busName)
	/// 
	/// - busName: 'all' or a bus name
	/// </summary>
	public class SequencerCommandMAUnpauseBus : BaseMasterAudioSequencerCommand {

		public void Start() {
			string busName = GetParameter(0);
			if (string.IsNullOrEmpty(busName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAUnpauseBus({1}): busName is blank", new object[] {DialogueDebug.Prefix, busName}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAUnpauseBus({1})", new object[] {DialogueDebug.Prefix, busName}));
				if (string.Equals(busName, AllKeyword)) {
					var busNames = MasterAudio.RuntimeBusNames;
					for (var i = 0; i < busNames.Count; i++) {
						MasterAudio.UnpauseBus(busNames[i]);
					}
				} else {
					MasterAudio.UnpauseBus(busName);
				}
			}
			Stop();
		}
		
	}
	 
}
