using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data")]
public class Character : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Image;
    public PersonalityTypes? Personality;
    public List<Skill> CurrentSkills;
    public int StressCapacity;
    public int CurrentStress;
    public int FocusPointCapacity;
    public int CurrentFocusPoints;
    public int Agility;
    public PriorityTypes? Specialty;
    public string Strain;
    public int Price;
    public string Biography;
    public Sprite Headshot;
    public Sprite SmallHeadshot;
}