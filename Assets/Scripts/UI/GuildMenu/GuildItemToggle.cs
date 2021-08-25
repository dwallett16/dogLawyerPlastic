using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildItemToggle : MonoBehaviour
{
    private Image Background;
    private Text[] ButtonLabels;

    private Button test;

    void Start() 
    {
        Background = gameObject.GetComponent<Image>();
        ButtonLabels = gameObject.GetComponentsInChildren<Text>();
        Background.enabled = false;
        test = GetComponent<Button>();
        //test.onClick.AddListener(log);
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

    public void log()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            Debug.Log("DOUBLE CLICK");
        }
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.clickCount == 1)
    //        Debug.Log("SINGLE CLICK");
    //    else if (eventData.clickCount >= 2)
    //        Debug.Log("DOUBLE CLICK");
    //}
}
