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

    public Case ActiveCase
    {
        get 
        {
            return activeCase;
        }
    }
    [SerializeField]
    private Case activeCase;

    public List<Character> PartyList
    {
        get 
        {
            return partyList;
        }
    }
    [SerializeField]
    private List<Character> partyList;

    public void AddEvidence(Evidence evidence) 
    {
        if(evidenceList.TrueForAll(e => e.Id != evidence.Id))
            evidenceList.Add(evidence);
    }

    public void AddSkill(Skill skill)
    {
        skillsList.Add(skill);
    }

    public void SetActiveCase(Case c)
    {
        activeCase = c;
    }

    public void AddPartyMember(Character character)
    {
        partyList.Add(character);
    }
}
