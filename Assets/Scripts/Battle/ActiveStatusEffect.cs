using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class ActiveStatusEffect
    {
        public ActiveStatusEffect(StatusEffect statusEffect, int numRounds)
        {
            _statusEffect = statusEffect;
            _roundsRemaining = numRounds;
            _hasBeenApplied = false;

            Debug.Log($"{ statusEffect.Name} has been activated.");
        }
        public StatusEffect StatusEffect { get { return _statusEffect; } }
        public int RoundsRemaining { get { return _roundsRemaining; } }
        public bool HasBeenApplied { get { return _hasBeenApplied; } }

        private StatusEffect _statusEffect;
        private int _roundsRemaining;
        private bool _hasBeenApplied;

        private int witAdjustment;
        private int resistanceAdjustment;
        private int enduranceAdjustment;
        private int passionAdjustment;
        private int persuasionAdjustment;

        private int stressPointAdjustment;
        private int focusPointAdjustment;
        private int focusPointCapacityAdjustment;

        private int totalWitAdjustment = 0;
        private int totalResistanceAdjustment = 0;
        private int totalEnduranceAdjustment = 0;
        private int totalPassionAdjustment = 0;
        private int totalPersuasionAdjustment = 0;
        private int totalFocusPointCapacityAdjustment = 0;

        public void DecreaseRoundsRemainingByOne()
        {
            _roundsRemaining--;
        }

        public void ApplyStandardEffect(CharacterBattleData characterBattleData)
        { 
            witAdjustment = _statusEffect.WitAdjustment;
            resistanceAdjustment = _statusEffect.ResistanceAdjustment;
            enduranceAdjustment = _statusEffect.EnduranceAdjustment;
            passionAdjustment = _statusEffect.PassionAdjustment;
            persuasionAdjustment = _statusEffect.PersuasionAdjustment;

            stressPointAdjustment = _statusEffect.StressPointAdjustment;
            focusPointAdjustment = _statusEffect.CurrentFocusPointAdjustment;
            focusPointCapacityAdjustment = _statusEffect.FocusPointCapacityAdjustment;

            if (_statusEffect.UsePercentages)
            {
                //Attributes
                if (_statusEffect.WitAdjustment != 0) witAdjustment = (int)Math.Round(characterBattleData.Wit * ( _statusEffect.WitAdjustment / (double)100));
                if (_statusEffect.ResistanceAdjustment != 0) resistanceAdjustment = (int)Math.Round(characterBattleData.Resistance * (_statusEffect.ResistanceAdjustment / (double)100));
                if (_statusEffect.EnduranceAdjustment != 0) enduranceAdjustment = (int)Math.Round(characterBattleData.Endurance * (_statusEffect.EnduranceAdjustment / (double)100));
                if (_statusEffect.PassionAdjustment != 0) passionAdjustment = (int)Math.Round(characterBattleData.Passion * (_statusEffect.PassionAdjustment / (double)100));
                if (_statusEffect.PersuasionAdjustment != 0) persuasionAdjustment = (int)Math.Round(characterBattleData.Persuasion * (_statusEffect.PersuasionAdjustment / (double)100));

                //Stats
                if (_statusEffect.StressPointAdjustment != 0) stressPointAdjustment = (int)Math.Round(characterBattleData.CurrentStress * (_statusEffect.StressPointAdjustment / (double)100));
                if (_statusEffect.CurrentFocusPointAdjustment != 0) focusPointAdjustment = (int)Math.Round(characterBattleData.CurrentFocusPoints * (_statusEffect.CurrentFocusPointAdjustment / (double)100));
                if (_statusEffect.FocusPointCapacityAdjustment != 0) focusPointCapacityAdjustment = (int)Math.Round(characterBattleData.FocusPointCapacity * (_statusEffect.CurrentFocusPointAdjustment / (double)100));
            }

            if (witAdjustment != 0)
            {
                characterBattleData.AdjustWit(witAdjustment);
                totalWitAdjustment += witAdjustment;
            }

            if (resistanceAdjustment != 0)
            {
                characterBattleData.AdjustResistance(resistanceAdjustment);
                totalResistanceAdjustment += resistanceAdjustment;
            }

            if (enduranceAdjustment != 0)
            {
                characterBattleData.AdjustEndurance(enduranceAdjustment);
                totalEnduranceAdjustment += enduranceAdjustment;
            }

            if (passionAdjustment != 0)
            {
                characterBattleData.AdjustPassion(passionAdjustment);
                totalPassionAdjustment += passionAdjustment;
            }

            if (persuasionAdjustment != 0)
            {
                characterBattleData.AdjustPersuasion(persuasionAdjustment);
                totalPersuasionAdjustment += persuasionAdjustment;
            }

            if (focusPointCapacityAdjustment != 0)
            {
                characterBattleData.AdjustFocusPointCapacity(focusPointCapacityAdjustment);
                totalFocusPointCapacityAdjustment += focusPointCapacityAdjustment;
            }


            if (stressPointAdjustment > 0)
            {
                characterBattleData.IncreaseStress(stressPointAdjustment);
            }
            else if (stressPointAdjustment < 0)
            {
                characterBattleData.ReduceStress(stressPointAdjustment * -1);
            }

            if (focusPointAdjustment > 0)
            {
                characterBattleData.IncreaseFocusPoints(focusPointAdjustment);
            }
            else if (focusPointAdjustment < 0)
            {
                characterBattleData.DecreaseFocusPoints(focusPointAdjustment * -1);
            }

            _hasBeenApplied = true;

            Debug.Log($"{_statusEffect.Name} applied to {characterBattleData.displayName}.");
        }

        private void RestoreAttributesAndFocusPointCapacity(CharacterBattleData characterBattleData)
        {
            characterBattleData.AdjustWit(-totalWitAdjustment);
            characterBattleData.AdjustEndurance(-totalEnduranceAdjustment);
            characterBattleData.AdjustPassion(-totalPassionAdjustment);
            characterBattleData.AdjustPersuasion(-totalPersuasionAdjustment);
            characterBattleData.AdjustResistance(-totalResistanceAdjustment);
            characterBattleData.AdjustFocusPointCapacity(-totalFocusPointCapacityAdjustment);
        }

        public void EndStatusEffect(CharacterBattleData characterBattleData)
        {
            if (!_statusEffect.DoNotRestoreOnExpiration)
            {
                RestoreAttributesAndFocusPointCapacity(characterBattleData);
            }
            characterBattleData.RemoveStatusEffect(this);
            Debug.Log($"{StatusEffect.Name} has ended.");
        }
    }
}
