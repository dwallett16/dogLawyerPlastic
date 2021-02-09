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

    public void MapFromScriptableObject(Character characterData) 
    {
        displayName = characterData.Name;
        type = characterData.Type;
        
        personality = characterData.Personality;
        stressCapacity = characterData.StressCapacity;
        focusPointCapacity = characterData.FocusPointCapacity;
        wit = characterData.Wit;
        resistance = characterData.Resistance;
        endurance = characterData.Endurance;
        passion = characterData.Passion;
        persuasion = characterData.Persuasion;
        if(characterData.Type != CharacterType.PlayerCharacter) {
            specialty = characterData.Specialty;
            skills = characterData.Skills;
        }
    }
}
