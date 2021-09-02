using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class ActiveStatusEffect
    {
        public ActiveStatusEffect(StatusEffects statusEffect, int numRounds)
        {
            _statusEffect = statusEffect;
            _roundsRemaining = numRounds;
        }
        public StatusEffects StatusEffect { get { return _statusEffect; } }
        public int RoundsRemaining { get { return _roundsRemaining; } }

        private StatusEffects _statusEffect;
        private int _roundsRemaining;

        public void DecreaseRoundsRemainingByOne()
        {
            _roundsRemaining--;
        }
    }
}
