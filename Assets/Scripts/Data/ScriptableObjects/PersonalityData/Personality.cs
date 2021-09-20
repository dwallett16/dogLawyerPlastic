using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Personality", menuName = "Personality")]
public class Personality : ScriptableObject
{
    public int Id;
    public ActionTypes[] Priorities = new ActionTypes[5];
}
