using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildItemToggle : MonoBehaviour
{
    private Text[] ButtonLabels;

    void Start() 
    {
        ButtonLabels = gameObject.GetComponentsInChildren<Text>();
    }

    void Update ()
    {
        var active = gameObject == EventSystem.current.currentSelectedGameObject;
        for(var i = 0; i < ButtonLabels.Length; i++) {
            ButtonLabels[i].color= active ? Color.white : Color.black;
        }
    }

}
