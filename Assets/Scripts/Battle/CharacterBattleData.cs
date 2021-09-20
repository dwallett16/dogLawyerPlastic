using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Battle;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;

public class CharacterBattleData : MonoBehaviour
{
    [NonSerialized]
    public string displayName;
    [NonSerialized]
    public CharacterType type;
    [NonSerialized]
    public Personality personality;
    [NonSerialized]
    public int StressCapacity;
    public int CurrentStress { get { return _currentStress; } }
    [NonSerialized]
    public int FocusPointCapacity;
    public int CurrentFocusPoints { get { return _currentFocusPoints; } }
    [NonSerialized]
    public int Wit;
    [NonSerialized]
    public int Resistance;
    [NonSerialized]
    public int Endurance;
    [NonSerialized]
    public int Passion;
    [NonSerialized]
    public int Persuasion;
    public int CurrentWit { get { return _currentWit; } }
    public int CurrentResistance { get { return _currentResistance; } }
    public int CurrentEndurance { get { return _currentEndurance; } }
    public int CurrentPassion { get { return _currentPassion; } }
    public int CurrentPersuasion { get { return _currentPersuasion; } }
    public int CurrentFocusPointCapacity { get { return _currentFocusPointCapacity; } }
    [NonSerialized]
    public AiPriorityTypes Specialty;
    [NonSerialized]
    public List<Skill> Skills;
    public IReadOnlyList<ActiveStatusEffect> ActiveStatusEffects { get { return _activeStatusEffects; } }

    private int _currentStress;
    private int _currentFocusPoints;
    private List<ActiveStatusEffect> _activeStatusEffects = new List<ActiveStatusEffect>();

    private int _currentWit;
    private int _currentResistance;
    private int _currentEndurance;
    private int _currentPassion;
    private int _currentPersuasion;
    private int _currentFocusPointCapacity;


    public void IncreaseStress(int points)
    {
        _currentStress += points;
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
        if (_currentFocusPoints > FocusPointCapacity)
            _currentFocusPoints = FocusPointCapacity;
    }

    public void DecreaseFocusPoints(int points)
    {
        _currentFocusPoints -= points;
        if (_currentFocusPoints < 0)
            _currentFocusPoints = 0;
    }

    public void AddStatusEffect(StatusEffect statusEffect, int statusEffectTurnCount)
    {
        if (!_activeStatusEffects.Exists(s => s.StatusEffect.Name == statusEffect.Name))
        {
            var newStatusEffect = new ActiveStatusEffect(statusEffect, statusEffectTurnCount);
            _activeStatusEffects.Add(newStatusEffect);

            if (newStatusEffect.StatusEffect.AdjustImmediately && !newStatusEffect.StatusEffect.NonstandardEffect)
            {
                newStatusEffect.ApplyStandardEffect(this);
            }
        }
    }

    public void RemoveStatusEffect(ActiveStatusEffect statusEffect)
    {
        _activeStatusEffects.Remove(statusEffect);
    }

    private void MapFromScriptableObject(Character characterData) 
    {
        displayName = characterData.Name;
        type = characterData.Type;
        
        personality = characterData.Personality;
        StressCapacity = characterData.StressCapacity;
        FocusPointCapacity = characterData.FocusPointCapacity;
        IncreaseFocusPoints(characterData.FocusPointCapacity);
        Wit = characterData.Wit;
        Resistance = characterData.Resistance;
        Endurance = characterData.Endurance;
        Passion = characterData.Passion;
        Persuasion = characterData.Persuasion;
        Skills = characterData.Skills;
        if (characterData.Type != CharacterType.PlayerCharacter) {
            Specialty = characterData.Specialty;
        }
    }

    public void InitializeCharacter(Character characterData)
    {
        MapFromScriptableObject(characterData);
        _activeStatusEffects = new List<ActiveStatusEffect>();
        _currentEndurance = Endurance;
        _currentPassion = Passion;
        _currentPersuasion = Persuasion;
        _currentResistance = Resistance;
        _currentWit = Wit;
        _currentFocusPointCapacity = FocusPointCapacity;
        _currentFocusPoints = FocusPointCapacity;
        _currentStress = 0;
    }

    public void AdjustWit(int adjustment)
    {
        _currentWit += adjustment;
        if (_currentWit <= 0) _currentWit = 0;
    }

    public void AdjustResistance(int adjustment)
    {
        _currentResistance += adjustment;
        if (_currentResistance <= 0) _currentResistance = 0;
    }

    public void AdjustEndurance(int adjustment)
    {
        _currentEndurance += adjustment;
        if (_currentEndurance <= 0) _currentEndurance = 0;
    }

    public void AdjustPassion(int adjustment)
    {
        _currentPassion += adjustment;
        if (_currentPassion <= 0) _currentPassion = 0;
    }

    public void AdjustPersuasion(int adjustment)
    {
        _currentPersuasion += adjustment;
        if (_currentPersuasion <= 0) _currentPersuasion = 0;
    }

    public void AdjustFocusPointCapacity(int adjustment)
    {
        _currentFocusPointCapacity += adjustment;
        if (_currentFocusPointCapacity <= 0) _currentFocusPointCapacity = 0;
        if (_currentFocusPoints > _currentFocusPointCapacity) _currentFocusPoints = _currentFocusPointCapacity;
    }
}
