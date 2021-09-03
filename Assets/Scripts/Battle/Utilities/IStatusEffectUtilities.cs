using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Utilities
{
    public interface IStatusEffectUtilities
    {
        void ProcessStatusEffects(CharacterBattleData characterBattleData);
    }
}
