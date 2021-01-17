using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBattleData : MonoBehaviour
{
    [NonSerialized]
    public string displayName;
    [NonSerialized]
    public CharacterType type;
    [NonSerialized]
    public PersonalityTypes personality;
    [NonSerialized]
    public int stressCapacity;
    [NonSerialized]
    public int currentStress;
    [NonSerialized]
    public int focusPointCapacity;
    [NonSerialized]
    public int currentFocusPoints;
    [NonSerialized]
    public int wit;
    [NonSerialized]
    public int resistance;
    [NonSerialized]
    public int endurance;
    [NonSerialized]
    public int passion;
    [NonSerialized]
    public int persuasion;
    [NonSerialized]
    public PriorityTypes specialty;
    [NonSerialized]
    public List<Skill> skills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
