using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    private static GameObject audioObject;
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (audioObject == null)
            audioObject = gameObject;
        else
            Destroy(gameObject);
    }
}
