using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildItemToggle : MonoBehaviour
{
    private Image Background;
    private Text[] ButtonLabels;

    void Start() 
    {
        Background = gameObject.GetComponent<Image>();
        ButtonLabels = gameObject.GetComponentsInChildren<Text>();
        Background.enabled = false;
    }

    void Update ()
    {
        var active = gameObject == EventSystem.current.currentSelectedGameObject;
        Background.enabled = active;
        for(var i = 0; i < ButtonLabels.Length; i++) {
            ButtonLabels[i].color= active ? Color.white : Color.black;
        }
        //if(active)Debug.Log(GetComponent<RectTransform>().localPosition);
    }
}
