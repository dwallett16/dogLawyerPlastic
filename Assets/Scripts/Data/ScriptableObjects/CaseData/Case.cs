using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Case", menuName = "Case Data")]
public class Case : ScriptableObject
{
    public int Id;
    public string Name;
    public Character Defendant;
    public List<Character> DefenseAttorneys;
    public int NumActiveDefenseAttorneys;
    public List<Evidence> EffectiveEvidence;
    public List<Evidence> RelevantEvidence;
    public List<Evidence> AllEvidence;
    public string CaseDescription;
    public Sprite Image;

}
