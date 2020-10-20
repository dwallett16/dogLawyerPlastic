using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class ConditionalObject : MonoBehaviour
{
    public string LuaVariableName;

    // Start is called before the first frame update
    void Start()
    {
        if(string.IsNullOrEmpty(LuaVariableName))
            return;

        var isActive = DialogueLua.GetVariable(LuaVariableName).asBool;
        gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
