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
    public string StatusEffect;
    public int FocusPointCost;
    public int RefreshRate;
    public List<StatusEffects> EffectsToRemove;
    public List<StatusEffects> EffectsToAdd;
    public int StatusEffectTurnCount;
    public string Description;
    public Animation SkillAnimation;
    public Animation TargetAnimation;
    public ActionTypes ActionType;
    public int Price;
}
