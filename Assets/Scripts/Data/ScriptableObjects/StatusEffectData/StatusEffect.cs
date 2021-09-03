using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.ScriptableObjects.StatusEffectData
{
    [CreateAssetMenu(fileName = "StatusEffect", menuName = "Status Effect Data")]
    public class StatusEffect : ScriptableObject
    {
        public int Id;
        public string Name;
        [Tooltip("Description of the nonstandard effect. This is for documentation only; no code will reference this value.")]
        [TextArea]
        public string EffectDescription = "Description of the nonstandard effect. This is for documentation only; no code will reference this value.";
        [Header("Settings")]
        [Tooltip("Adjustment values are percentages of the current Stat or Attribute instead of a raw value")]
        public bool UsePercentages;
        [Tooltip("Adjustments are applied on each turn. Amount of turns is determined by the Skill that applies the effect.")]
        public bool IsRecurring;
        [Tooltip("Adjustments happen immediately upon applying the effect. Otherwise, adjustments happen at the end of the recipient's turn.")]
        public bool AdjustImmediately;
        [Tooltip("Nonstandard status effects ignore all adjustments and settings. Effects are hard coded.")]
        public bool NonstandardEffect;
        [Header("Adjustments")]
        public int ResistanceAdjustment;
        public int EnduranceAdjustment;
        public int PassionAdjustment;
        public int PersuasionAdjustment;
        public int WitAdjustment;
        public int StressPointAdjustment;
        public int FocusPointAdjustment;
    }
}
