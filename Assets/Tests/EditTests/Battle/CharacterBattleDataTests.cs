using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using NUnit.Framework;

namespace Battle
{
    class CharacterBattleDataTests
    {
        [Test]
        public void AddStatusEffectWithAdjustImmediatelyCheckedAppliesEffectWhenAdded()
        {
            var statusEffect = TestDataFactory.CreateStatusEffect(false, false, true, false, 10, 0, 0, 0, 0, 0, 0, 0);

            var character = new Character()
            {
                Resistance = 10,
                Endurance = 10,
                Passion = 10,
                Persuasion = 10,
                Wit = 10,
                FocusPointCapacity = 10
            };
            var characterBattleData = new CharacterBattleData();
            characterBattleData.InitializeCharacter(character);

            characterBattleData.AddStatusEffect(statusEffect, 10);

            Assert.AreEqual(20, characterBattleData.CurrentResistance);
        }

        [Test]
        public void AddStatusEffectWithAdjustImmediatelyNotCheckedDoesNotApplyEffectWhenAdded()
        {
            var statusEffect = TestDataFactory.CreateStatusEffect(false, false, false, false, 10, 0, 0, 0, 0, 0, 0, 0);

            var character = new Character()
            {
                Resistance = 10,
                Endurance = 10,
                Passion = 10,
                Persuasion = 10,
                Wit = 10,
                FocusPointCapacity = 10
            };
            var characterBattleData = new CharacterBattleData();
            characterBattleData.InitializeCharacter(character);

            characterBattleData.AddStatusEffect(statusEffect, 10);

            Assert.AreEqual(10, characterBattleData.CurrentResistance);
        }
    }
}
