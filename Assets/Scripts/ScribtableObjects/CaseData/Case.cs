using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : ScriptableObject
{
    public int Id;
    public string Name;
    public List<Character> Defendant;
    public List<Character> DefenseAttorneys;
    public int NumActiveDefenseAttorneys;
    public List<Evidence> EffectiveEvidence;
    public List<Evidence> RelevantEvidence;
    public List<Evidence> AllEvidence;
    public string CaseDescription;
    public Sprite Image;
    //public NPC GuiltyNpc;

}
