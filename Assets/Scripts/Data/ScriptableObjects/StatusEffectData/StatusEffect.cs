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
        [Tooltip("Description of the effect. This is for documentation only; no code will reference this value.")]
        [TextArea]
        public string EffectDescription = "Description of the effect. This is for documentation only; no code will reference this value.";
        [Header("Settings")]
        [Tooltip("Adjustment values are percentages of the current value of the Stat or Attribute instead of a raw value")]
        public bool UsePercentages;
        [Tooltip("Adjustments are applied on each turn. Amount of turns is determined by the Skill that applies the effect.  If false, the effect will be applied once. This can be combined with AdjustImmediately.")]
        public bool IsRecurring;
        [Tooltip("Adjustments happen immediately upon receiving the effect. This can be combined with IsRecurring.")]
        public bool AdjustImmediately;
        [Tooltip("All adjustments to Attributes or Focus Point Capacity are permanent even after the effect expires.")]
        public bool DoNotRestoreOnExpiration;
        [Tooltip("Nonstandard status effects ignore all adjustments and settings. Effects are hard coded.")]
        public bool NonstandardEffect;
        [Header("Attribute Adjustments")]
        public int ResistanceAdjustment;
        public int EnduranceAdjustment;
        public int PassionAdjustment;
        public int PersuasionAdjustment;
        public int WitAdjustment;
        [Header("Stat Adjustments")]
        public int StressPointAdjustment;
        public int CurrentFocusPointAdjustment;
        public int FocusPointCapacityAdjustment;
    }
}
