using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalItemToggle : MonoBehaviour
{
    private Image Background;
    private Text ButtonText;

    void Start() 
    {
        Background = gameObject.GetComponent<Image>();
        ButtonText = gameObject.GetComponentInChildren<Text>();
        Background.enabled = false;
    }

    void Update ()
    {
        var active = gameObject == EventSystem.current.currentSelectedGameObject;
        Background.enabled = active;
        ButtonText.color = active ? Color.white : Color.black;
    }
}
