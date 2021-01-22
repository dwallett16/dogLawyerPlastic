using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAUnpauseEverything()
	/// </summary>
	public class SequencerCommandMAUnpauseEverything : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAUnpauseEverything()");
			MasterAudio.UnpauseEverything();
		}
		
	}
	
}
