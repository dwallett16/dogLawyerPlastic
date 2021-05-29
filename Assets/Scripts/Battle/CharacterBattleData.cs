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
    public AiPriorityTypes specialty;
    [NonSerialized]
    public List<Skill> skills;
    [NonSerialized]
    public List<StatusEffects> activeStatusEffects;

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
        skills = characterData.Skills;
        if (characterData.Type != CharacterType.PlayerCharacter) {
            specialty = characterData.Specialty;
        }
        activeStatusEffects = new List<StatusEffects>();
    }
}
