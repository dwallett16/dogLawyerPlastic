using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using DarkTonic.MasterAudio;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Sequencer command MAResumePlaylist([playlistControllerName])
    /// 
    /// - playlistControllerName: blank, 'all', or a playlist controller name
    /// </summary>
    public class SequencerCommandMAResumePlaylist : BaseMasterAudioSequencerCommand
    {

        public void Start()
        {
            string playlistControllerName = GetParameter(0);
            if (DialogueDebug.LogInfo) Debug.Log(string.Format("{0}: Sequencer: MAResumePlaylist({1})", new object[] { DialogueDebug.Prefix, playlistControllerName }));
            if (string.Equals(playlistControllerName, AllKeyword))
            {
                MasterAudio.UnpauseAllPlaylists();
                //var pcs = PlaylistController.Instances;
                //for (var i = 0; i < pcs.Count; i++)
                //{
                //    MasterAudio.Un
                //    //MasterAudio.ResumePlaylist(pcs[i].name);
                //}
            }
            else
            {
                if (string.IsNullOrEmpty(playlistControllerName))
                {
                    MasterAudio.UnpausePlaylist();
                }
                else
                {
                    MasterAudio.UnpausePlaylist(playlistControllerName);
                }
            }
            Stop();
        }

    }

}
