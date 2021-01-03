using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour
{

    private string displayName {get; set;}
    private CharacterType type {get; set;}
    private int stressCapacity {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapFromData(Character characterData)
    {
        displayName = characterData.Name;
        type = characterData.Type;
        stressCapacity = characterData.StressCapacity;
    }
}
