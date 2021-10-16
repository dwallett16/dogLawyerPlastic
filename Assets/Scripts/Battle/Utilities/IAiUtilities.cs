using System;


namespace Assets.Scripts.Battle.Utilities
{
    public interface IAiUtilities
    {
        bool ProcessCondition(Condition condition, BattleController battleController, CharacterBattleData selfBattleData);
    }
}
