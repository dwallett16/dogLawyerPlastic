using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemToggle : MonoBehaviour
{
    private Text ButtonText;
    public Color ActiveColor;
    public Color PassiveColor;
    private Color activeColor;
    private Color passiveColor;

    void Start() 
    {
        ButtonText = gameObject.GetComponentInChildren<Text>();
        activeColor = new Color(ActiveColor.r, ActiveColor.g, ActiveColor.b, 1);
        passiveColor = new Color(PassiveColor.r, PassiveColor.g, PassiveColor.b, 1);
    }

    void Update ()
    {
        var active = gameObject == EventSystem.current.currentSelectedGameObject;
        ButtonText.color = active ? activeColor : passiveColor;
    }
}
