using System.Collections.Generic;
using UnityEngine;

public class ButtonData: MonoBehaviour
{
    public int Id { get; set; }
    public Sprite Image { get; set; }
    public string Description { get; set; }
    public string StressCapacity { get; set; }
    public string FocusPoints { get; set; }
    public List<Skill> Skills { get; set; }
    public AiPriorityTypes SkillType {get; set;}
    public int FpCost {get; set;}
    public int LatentPower {get; set;}
    public int Price {get;set;}
    public string ExamineConversation {get; set;}
}
