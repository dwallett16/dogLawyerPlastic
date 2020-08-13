using System.Collections.Generic;

public abstract class Inventory
{
    public Inventory() 
    {
        skillsList = new List<Skill>();
        partyList = new List<Character>();
    }

    public List<Skill> SkillsList
    {
        get 
        {
            return skillsList;
        }
    }
    private List<Skill> skillsList;

    public List<Character> PartyList
    {
        get 
        {
            return partyList;
        }
    }
    private List<Character> partyList;

    public void AddSkill(Skill skill)
    {
        skillsList.Add(skill);
    }

    public void AddPartyMember(Character character)
    {
        partyList.Add(character);
    }
}
