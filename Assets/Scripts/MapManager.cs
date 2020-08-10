using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas/OfficeButton"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
