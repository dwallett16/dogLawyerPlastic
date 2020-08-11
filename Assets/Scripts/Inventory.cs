using System.Collections.Generic;
using UnityEngine;

public class Inventory
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

    public List<Case> ActiveCases
    {
        get 
        {
            return activeCases;
        }
    }
    [SerializeField]
    private List<Case> activeCases;

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

    public void AddActiveCase(Case c)
    {
        activeCases.Add(c);
    }

    public void AddPartyMember(Character character)
    {
        partyList.Add(character);
    }
}
