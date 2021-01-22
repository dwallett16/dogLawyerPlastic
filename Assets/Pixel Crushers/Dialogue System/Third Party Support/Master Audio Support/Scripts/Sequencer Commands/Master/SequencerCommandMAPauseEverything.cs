using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAPauseEverything()
	/// </summary>
	public class SequencerCommandMAPauseEverything : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAPauseEverything()");
			MasterAudio.PauseEverything();
		}
		
	}
	
}
