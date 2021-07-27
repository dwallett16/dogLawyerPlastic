using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    public int currentStress { get { return _currentStress; } }
    [NonSerialized]
    public int focusPointCapacity;
    public int currentFocusPoints { get { return _currentFocusPoints; } }
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
    public List<StatusEffects> activeStatusEffects { get { return _activeStatusEffects; } }

    private int _currentStress;
    private int _currentFocusPoints;
    private List<StatusEffects> _activeStatusEffects = new List<StatusEffects>();

    public void IncreaseStress(int points)
    {
        _currentStress += points;
        if (_currentStress > stressCapacity)
            _currentStress = stressCapacity;
    }

    public void ReduceStress(int points)
    {
        _currentStress -= points;
        if (_currentStress < 0)
            _currentStress = 0;
    }

    public void IncreaseFocusPoints(int points)
    {
        _currentFocusPoints += points;
        if (_currentFocusPoints > focusPointCapacity)
            _currentFocusPoints = focusPointCapacity;
    }

    public void DecreaseFocusPoints(int points)
    {
        _currentFocusPoints -= points;
        if (_currentFocusPoints < 0)
            _currentFocusPoints = 0;
    }

    public void AddStatusEffect(StatusEffects statusEffect)
    {
        if (!_activeStatusEffects.Any(x => x == statusEffect))
            _activeStatusEffects.Add(statusEffect);
    }

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
        _activeStatusEffects = new List<StatusEffects>();
    }
}
