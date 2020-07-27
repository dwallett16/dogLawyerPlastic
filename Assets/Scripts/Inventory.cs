using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Evidence> EvidenceList { 
        get 
        {
            return evidenceList;
        }
    }
    [SerializeField]
    private List<Evidence> evidenceList;

    public List<Skill> SkillsList
    {
        get 
        {
            return skillsList;
        }
    }
    [SerializeField]
    private List<Skill> skillsList;

    public void AddEvidence(Evidence evidence) 
    {
        if(evidenceList.TrueForAll(e => e.Id != evidence.Id))
            evidenceList.Add(evidence);
    }

    public void AddSkill(Skill skill)
    {
        skillsList.Add(skill);
    }
}
