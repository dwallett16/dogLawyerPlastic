using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAUnpauseMixer()
	/// </summary>
	public class SequencerCommandMAUnpauseMixer : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAUnpauseMixer()");
			MasterAudio.UnpauseMixer();
		}
		
	}
	
}
