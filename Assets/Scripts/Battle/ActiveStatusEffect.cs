using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;

namespace Assets.Scripts.Battle
{
    public class ActiveStatusEffect
    {
        public ActiveStatusEffect(StatusEffect statusEffect, int numRounds)
        {
            _statusEffect = statusEffect;
            _roundsRemaining = numRounds;
        }
        public StatusEffect StatusEffect { get { return _statusEffect; } }
        public int RoundsRemaining { get { return _roundsRemaining; } }
        public bool HasBeenApplied { get { return _hasBeenApplied; } }

        private StatusEffect _statusEffect;
        private int _roundsRemaining;
        private bool _hasBeenApplied;

        public void DecreaseRoundsRemainingByOne()
        {
            _roundsRemaining--;
        }
    }
}
