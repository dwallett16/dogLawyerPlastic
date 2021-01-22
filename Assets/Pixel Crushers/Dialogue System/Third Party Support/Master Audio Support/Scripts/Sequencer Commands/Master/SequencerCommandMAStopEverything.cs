using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAStopEverything()
	/// </summary>
	public class SequencerCommandMAStopEverything : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAStopEverything()");
			MasterAudio.StopEverything();
		}
		
	}
	
}
