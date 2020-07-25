using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : ScriptableObject
{
    public int Id;
    public string Name;
    public Case ParentCase;
    public Sprite Image;
    public string Description;
    public bool CanBeExamined;
    public string JournalDialogue;
}
