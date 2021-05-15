using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataFactory
{
    public static Case CreateCase(int id, string name = null, List<Character> defenseAttorneys = null, Character defendant = null) {
        var testCase = new Case {
            Id = id,
            CaseDescription = $"case {id}",
            Name = name == null ? $"case {id}" : name
        };
        var allEvidence = new List<Evidence> { CreateEvidence(0, testCase), CreateEvidence(1, testCase), CreateEvidence(2, testCase)  };
        testCase.AllEvidence = allEvidence;

        testCase.Defendant = defendant == null ? CreateCharacter(0, CharacterType.DefendantCharacter) : defendant;
        testCase.DefenseAttorneys = defenseAttorneys == null ? new List<Character> { CreateCharacter(1, CharacterType.DefenseCharacter) } : defenseAttorneys;
        testCase.EffectiveEvidence = new List<Evidence> { allEvidence[0] };
        testCase.RelevantEvidence = new List<Evidence> { allEvidence[1] };

        return testCase;
    }

    public static Character CreateCharacter(int id, CharacterType type, List<Skill> skills = null, PersonalityTypes personality = PersonalityTypes.None,
     AiPriorityTypes specialty = AiPriorityTypes.None, int wit = 10) {
        var character = new Character {
            Id = id,
            Name = $"character {id}",
            Type = type,
            Personality = personality,
            StressCapacity = 10,
            FocusPointCapacity = 10,
            Endurance = 10,
            Passion = 10,
            Persuasion = 10,
            Resistance = 10,
            Wit = wit,
            Specialty = specialty,
            Price = 100,
            JournalDescription = $"description for character {id}"
        };
        var battlePrefab = new GameObject();
        battlePrefab.AddComponent<CharacterBattleData>();
        character.BattlePrefab = battlePrefab;
        character.Skills = skills == null ? new List<Skill> {CreateSkill(0)} : skills;
        return character;
    }

    public static Skill CreateSkill(int id, SkillTarget target = SkillTarget.DefenseAttorneys, AiPriorityTypes type = AiPriorityTypes.Offense) {
        var skill = new Skill {
            Id = id,
            Name = $"Skill {id}",
            Target = target,
            AiPriorityType = type,
            Power = 10,
            FocusPointCost = 2,
            RefreshRate = 0,
            Price = 100,
            Description = $"skill {id}"
        };
        return skill;
    }

    public static Evidence CreateEvidence(int id, Case parentCase, bool canBeExamined = true) {
        var evidence = new Evidence {
            Id = id,
            CanBeExamined = canBeExamined,
            Description = $"evidence with id {id}",
            Name = $"evidence {id}",
            ParentCase = parentCase
        };
        return evidence;
    }
}