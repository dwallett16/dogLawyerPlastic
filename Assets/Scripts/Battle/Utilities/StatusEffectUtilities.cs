using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Utilities
{
    public class StatusEffectUtilities : IStatusEffectUtilities
    {
        public static IStatusEffectUtilities Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatusEffectUtilities();
                }
                return instance;
            }
        }
        private static IStatusEffectUtilities instance;
        public void ProcessStatusEffects(CharacterBattleData characterBattleData)
        {
            throw new NotImplementedException();
        }
    }
}
