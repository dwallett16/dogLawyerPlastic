using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Sequencer command MADisableDucking()
	/// </summary>
	public class SequencerCommandMADisableDucking : BaseMasterAudioSequencerCommand {

		public void Start() {
			if (DialogueDebug.LogInfo) Debug.Log(DialogueDebug.Prefix + ": Sequencer: MADisableDucking()");
			MasterAudio.Instance.EnableMusicDucking = false;
		}
		
	}
	 
}
