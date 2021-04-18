using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData
{
    public string ButtonAction;
    public GameObject CurrentCombatant;
    public IAction Action;
    public CharacterBattleData CurrentCombatantBattleData;
    public Skill SelectedSkill;
    public GameObject Target;
}
