using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAudio : MonoBehaviour
{
    public AudioClip[] Bgm;
    public AudioClip[] Sfx;
    public int InitialBgm = 0;
    public bool PlayBgmAtStart = true;
    public bool ContinueBgmFromLastScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayBgmAtStart) {
            if (ContinueBgmFromLastScene && !SoundManager.Instance.BgmIsPlaying()) {
                SoundManager.Instance.BgmPlay(Bgm[InitialBgm]);
            }
            else if (!ContinueBgmFromLastScene) {
                SoundManager.Instance.BgmPlay(Bgm[InitialBgm]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
