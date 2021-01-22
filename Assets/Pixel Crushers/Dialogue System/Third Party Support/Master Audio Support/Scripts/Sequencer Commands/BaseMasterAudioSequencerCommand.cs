using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	/// <summary>
	/// Base master audio sequencer command. This just defines a constant "all"
	/// used by many of the commands.
	/// </summary>
	public class BaseMasterAudioSequencerCommand : SequencerCommand {

		public const string AllKeyword = "all";

	}
	 
}
