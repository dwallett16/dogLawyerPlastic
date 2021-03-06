using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill Data")]
public class Skill : ScriptableObject
{
    public int Id;
    public string Name;
    public SkillTarget Target;
    public AiPriorityTypes AiPriorityType;
    public SkillTypes Type;
    public int Power;
    public int FocusPointCost;
    public int RefreshRate;
    public List<StatusEffect> EffectsToRemove;
    public List<StatusEffect> EffectsToAdd;
    public int StatusEffectTurnCount;
    public string Description;
    public Animation SkillAnimation;
    public Animation TargetAnimation;
    public ActionTypes ActionType;
    public int Price;
    public int Likelihood;
}
