using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAUnmuteEverything()
	/// </summary>
	public class SequencerCommandMAUnmuteEverything : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAUnmuteEverything()");
			MasterAudio.UnmuteEverything();
		}
		
	}
	
}
