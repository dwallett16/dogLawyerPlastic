using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// This script presents a button for each Master Audio sequencer command
    /// so you can test their functionality.
    /// </summary>
    public class MasterAudioExample : MonoBehaviour
    {

        public string busName = string.Empty;
        public string fadeTime = "1";
        public string soundGroupName = "Blast";
        public string targetVolume = "1";
        public string riseVolumeStart = "0";
        public string clipName = string.Empty;
        public string playlistControllerName = "PlaylistControllerOrchestra";
        public string subject = string.Empty;
        public string customEventName = string.Empty;

        private const float LabelWidth = 200f;
        private const float FieldWidth = 300f;
        private const float AllButtonWidth = 40f;

        private Vector2 scrollPosition = Vector2.zero;

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label("Master Audio Sequencer Command Test");

            //--- Parameters:
            busName = DrawLabeledFieldWithAllButton("busName", busName);
            soundGroupName = DrawLabeledFieldWithAllButton("soundGroupName", soundGroupName);
            clipName = DrawLabeledFieldWithAllButton("clipName", clipName);
            playlistControllerName = DrawLabeledFieldWithAllButton("playlistControllerName", playlistControllerName);
            targetVolume = DrawLabeledField("targetVolume", targetVolume);
            riseVolumeStart = DrawLabeledField("risingVolumeStart", riseVolumeStart);
            fadeTime = DrawLabeledField("fadeTime", fadeTime);
            subject = DrawLabeledField("subject", subject);
            customEventName = DrawLabeledField("customEventName", customEventName);

            GUILayout.BeginHorizontal();

            //--- Everything:
            GUILayout.BeginVertical();
            DrawSequenceButton("MAMuteEverything()");
            DrawSequenceButton("MAUnmuteEverything()");
            DrawSequenceButton("MAPauseEverything()");
            DrawSequenceButton("MAUnpauseEverything()");
            DrawSequenceButton("MAStopEverything()");
            DrawSequenceButton("MASetMasterVolume(targetVolume)");
            DrawSequenceButton("MAFireCustomEvent(customEventName)");
            //--- Mixer:
            DrawSequenceButton("MAPauseMixer()");
            DrawSequenceButton("MAUnpauseMixer()");
            DrawSequenceButton("MAStopMixer()");
            //--- Bus:
            DrawSequenceButton("MAFadeBus(busName, targetVolume, fadeTime)");
            DrawSequenceButton("MASetBusVolume(busName, targetVolume)");
            DrawSequenceButton("MAMuteBus(busName)");
            DrawSequenceButton("MAUnmuteBus(busName)");
            DrawSequenceButton("MAPauseBus(busName)");
            DrawSequenceButton("MAUnpauseBus(busName)");
            DrawSequenceButton("MASoloBus(busName)");
            DrawSequenceButton("MAUnsoloBus(busName)");
            DrawSequenceButton("MAStopBus(busName)");
            GUILayout.EndVertical();

            //--- Group/Ducking:
            GUILayout.BeginVertical();
            DrawSequenceButton("MASetGroupVolume(soundGroupName, targetVolume)");
            DrawSequenceButton("MAFadeGroup(soundGroupName, targetVolume, fadeTime)");
            DrawSequenceButton("MAFadeOutAllOfSound(soundGroupName, fadeTime)");
            DrawSequenceButton("MAStopAllOfSound(soundGroupName)");
            DrawSequenceButton("MAStopTransformSound(subject, soundGroupName)");
            DrawSequenceButton("MAMuteGroup(soundGroupName)");
            DrawSequenceButton("MAUnmuteGroup(soundGroupName)");
            DrawSequenceButton("MAPauseGroup(soundGroupName)");
            DrawSequenceButton("MAUnpauseGroup(soundGroupName)");
            DrawSequenceButton("MASoloGroup(soundGroupName)");
            DrawSequenceButton("MAUnsoloGroup(soundGroupName)");
            DrawSequenceButton("MASoloGroup(soundGroupName)");
            DrawSequenceButton("MAAddToDucking(soundGroupName, riseVolumeStart)");
            DrawSequenceButton("MARemoveFromDucking(soundGroupName)");
            DrawSequenceButton("MAEnableDucking())");
            DrawSequenceButton("MADisableDucking())");
            DrawSequenceButton("MAPlaySound(soundGroupName, clipName)");
            GUILayout.EndVertical();

            //--- Playlist:
            GUILayout.BeginVertical();
            DrawSequenceButton("MAPlaylistClip(clipName, playlistControllerName)");
            DrawSequenceButton("MANextPlaylistClip(playlistControllerName)");
            DrawSequenceButton("MARandomPlaylistClip(playlistControllerName)");
            DrawSequenceButton("MASetPlaylistVolume(targetVolume)");
            DrawSequenceButton("MAFadePlaylist(targetVolume, fadeTime, playlistControllerName)");
            DrawSequenceButton("MAMutePlaylist(playlistControllerName)");
            DrawSequenceButton("MAUnmutePlaylist(playlistControllerName)");
            DrawSequenceButton("MAPausePlaylist(playlistControllerName)");
            DrawSequenceButton("MAStopPlaylist(playlistControllerName)");
            DrawSequenceButton("MAResumePlaylist(playlistControllerName)");
            GUILayout.EndHorizontal();

            GUILayout.EndScrollView();
        }
        private string DrawLabeledField(string label, string value)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(LabelWidth));
            string result = GUILayout.TextField(value, GUILayout.Width(FieldWidth));
            GUILayout.EndHorizontal();
            return result;
        }

        private string DrawLabeledFieldWithAllButton(string label, string value)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(LabelWidth));
            string result = GUILayout.TextField(value, GUILayout.Width(FieldWidth));
            if (GUILayout.Button("All", GUILayout.Width(AllButtonWidth)))
            {
                result = "all";
            }
            GUILayout.EndHorizontal();
            return result;
        }

        private void DrawSequenceButton(string sequence)
        {
            if (GUILayout.Button(sequence))
            {
                DialogueManager.PlaySequence(ReplaceVariables(sequence));
            }
        }

        private string ReplaceVariables(string sequence)
        {
            return sequence.Replace("busName", busName).
                    Replace("soundGroupName", soundGroupName).
                    Replace("clipName", clipName).
                    Replace("playlistControllerName", playlistControllerName).
                    Replace("fadeTime", fadeTime).
                    Replace("targetVolume", targetVolume).
                    Replace("riseVolumeStart", riseVolumeStart).
                    Replace("subject", subject).
                    Replace("customEventName", customEventName);
        }

    }

}
