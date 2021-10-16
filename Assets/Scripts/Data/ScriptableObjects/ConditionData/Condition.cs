using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Condition")]
public class Condition: ScriptableObject
{
    public int Id;
    public string Name;
    public ActionTypes AffectedPriority;
    [Header("Individual Stats")]
    public CharacterConditionStats Self;
    public CharacterConditionStats DefenseAttorney2;
    public CharacterConditionStats Prosecutor1;
    public CharacterConditionStats Prosecutor2;
    public SubCondition DefenseTeamStressTotal;
    public SubCondition ProsecutorTeamStressTotal;
    public SubCondition DefenseTeamFocusPointsTotal;
    public SubCondition ProsecutorTeamFocusPointsTotal;
    public SubCondition JuryPoints;

    public Condition()
    {
        Self = new CharacterConditionStats
        {
            Stress = new SubCondition(),
            FocusPoints = new SubCondition()
        };
        DefenseAttorney2 = new CharacterConditionStats
        {
            Stress = new SubCondition(),
            FocusPoints = new SubCondition()
        };
        Prosecutor1 = new CharacterConditionStats
        {
            Stress = new SubCondition(),
            FocusPoints = new SubCondition()
        };
        Prosecutor2 = new CharacterConditionStats
        {
            Stress = new SubCondition(),
            FocusPoints = new SubCondition()
        };
        ProsecutorTeamStressTotal = new SubCondition();
        ProsecutorTeamFocusPointsTotal = new SubCondition();
        DefenseTeamStressTotal = new SubCondition();
        DefenseTeamFocusPointsTotal = new SubCondition();
        JuryPoints = new SubCondition();
    }
}

[Serializable]
public class CharacterConditionStats
{
    public SubCondition Stress;
    public SubCondition FocusPoints;
}

[Serializable]
public class SubCondition
{
    [Tooltip("Check this if this sub-condition should be evaluated.")]
    public bool Evaluate;
    [Tooltip("The minimum value for this sub-condition to evaluate to true.")]
    public int Value;
    [Tooltip("By default, greater-than-or-equal results in true. This changes it to less-than-or-equal.")]
    public bool LessThan;
    [Tooltip("This sub-condition will only evaluate to true if every other sub-condition with this checked also evaluate to true.")]
    public bool And;
}
