using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data")]
public class Character : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Image;
    public Material Material;
    public SkeletonDataAsset SkeletonData;
    public CharacterType Type;
    public PersonalityTypes Personality;
    public List<Skill> Skills;
    public int StressCapacity;
    public int FocusPointCapacity;
    public int Wit;
    public int Resistance;
    public int Endurance;
    public int Passion;
    public int Persuasion;
    public PriorityTypes Specialty;
    public int Price;
    public Sprite Headshot;
    public Sprite SmallHeadshot;
    public string JournalDescription;
    public GameObject BattlePrefab;
}