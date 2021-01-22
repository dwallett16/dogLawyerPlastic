using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MAMuteEverything()
	/// </summary>
	public class SequencerCommandMAMuteEverything : BaseMasterAudioSequencerCommand {

		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAMuteEverything()");
			MasterAudio.MuteEverything();
		}
		
	}
	 
}
