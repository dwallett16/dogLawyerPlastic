using System.Collections.Generic;
using System.Linq;

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

    public void RemovePartyMemberById(int id)
    {
        partyList.RemoveAll(x =>x.Id == id);
    }

    public void RemoveSkillById(int id)
    {
        skillsList.RemoveAll(x => x.Id == id);
    }

    public Character GetCharacterById(int id)
    {
        return partyList.First(x => x.Id == id);
    }

    public Skill GetSkillById(int id)
    {
        return skillsList.First(x => x.Id == id);
    }
}
