using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameObject gameData;
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (gameData == null)
            gameData = gameObject;
        else
            Destroy(gameObject);
    }

}
