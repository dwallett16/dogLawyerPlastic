using System.Collections.Generic;
using System.Linq;

public abstract class Inventory
{
    public Inventory() 
    {
        skillsList = new List<Skill>();
        partyList = new List<Character>();
    }

    public IReadOnlyList<Skill> SkillsList
    {
        get 
        {
            return skillsList;
        }
    }
    private List<Skill> skillsList;

    public IReadOnlyList<Character> PartyList
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
        var p = partyList.First(x => x.Id == id);
        partyList.RemoveAt(partyList.IndexOf(p));
    }

    public void RemoveSkillById(int id)
    {
        var s = skillsList.First(x => x.Id == id);
        skillsList.RemoveAt(skillsList.IndexOf(s));
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
