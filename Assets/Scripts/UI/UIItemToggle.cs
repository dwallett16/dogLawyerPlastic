using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemToggle : MonoBehaviour
{
    private Image Background;
    private Text ButtonText;
    public Color ActiveColor;
    public Color PassiveColor;
    private Color activeColor;
    private Color passiveColor;

    void Start() 
    {
        Background = gameObject.GetComponent<Image>();
        ButtonText = gameObject.GetComponentInChildren<Text>();
        Background.enabled = false;
        activeColor = new Color(ActiveColor.r, ActiveColor.g, ActiveColor.b, 1);
        passiveColor = new Color(PassiveColor.r, PassiveColor.g, PassiveColor.b, 1);
        
    }

    void Update ()
    {
        var temp = Color.black;
        var temp2 = Color.white;
        var active = gameObject == EventSystem.current.currentSelectedGameObject;
        Background.enabled = active;
        ButtonText.color = active ? activeColor : passiveColor;
    }
}
