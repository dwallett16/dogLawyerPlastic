using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAEnableDucking()
	/// </summary>
	public class SequencerCommandMAEnableDucking : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MAEnableDucking()");
			MasterAudio.Instance.EnableMusicDucking = true;
		}
		
	}
	
}
