using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	/// <summary>
	/// Sequencer command MAVariationChangePitch(soundGroupName, variationName, pitch)
	/// 
	/// - soundGroupName: sound group name
	/// - variationName: 'all' or a variation name
	/// - pitch: -3..3
	/// </summary>
	public class SequencerCommandMAVariationChangePitch : BaseMasterAudioSequencerCommand {
		
		public void Start() {
			string soundGroupName = GetParameter(0);
			string variationName = GetParameter(1);
			float pitch = GetParameterAsFloat (2);
			if (string.IsNullOrEmpty(soundGroupName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAVariationChangePitch({1}, {2}, {3}): soundGroupName is blank", new object[] {DialogueDebug.Prefix, soundGroupName, variationName, pitch}));
			} else if (string.Equals(soundGroupName, AllKeyword)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAVariationChangePitch({1}, {2}, {3}): soundGroupName can't be 'all' for this command", new object[] {DialogueDebug.Prefix, soundGroupName, variationName, pitch}));
			} else if (string.IsNullOrEmpty(variationName)) {
				if (DialogueDebug.LogWarnings) Debug.LogWarning(string.Format("{0}: Sequencer: MAVariationChangePitch({1}, {2}, {3}): variationName is blank", new object[] {DialogueDebug.Prefix, soundGroupName, variationName, pitch}));
			} else {
				if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAVariationChangePitch({1}, {2}, {3})", new object[] {DialogueDebug.Prefix, soundGroupName, variationName, pitch}));
				var childName = string.IsNullOrEmpty(variationName) ? null : variationName;
				var allVariations = string.Equals(variationName, AllKeyword);
				MasterAudio.ChangeVariationPitch(soundGroupName, allVariations, childName, pitch);
			}
			Stop();
		}
		
	}
	
}
