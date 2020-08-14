using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SfxSource;
    public AudioSource BgmSource;

    public static SoundManager Instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    void Start() {

    }

    public void SfxPlay(AudioClip clip) {
        SfxSource.clip = clip;
        SfxSource.Play();
    }

    public void BgmPlay(AudioClip clip) {
        BgmSource.clip = clip;
        BgmSource.Play();
    }

    public bool BgmIsPlaying() {
        return BgmSource.isPlaying;
    }

    public void BgmStop() {
        BgmSource.Stop();
    }
}
