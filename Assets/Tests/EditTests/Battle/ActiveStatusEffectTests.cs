using Assets.Scripts.Battle;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    class ActiveStatusEffectTests
    {
        [Test]
        public void ApplyStandardEffectDefaultSettingsPassionIs0NoStatAdjustmentAttributesOtherThanPassionAreAdjusted()
        {
            var statusEffect = CreateStatusEffect(false, false, false, false, 10, 10, 0, 10, 10, 0, 0, 0);

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
            

            var activeEffect = new ActiveStatusEffect(statusEffect, 5);

            activeEffect.ApplyStandardEffect(characterBattleData);

            Assert.AreEqual(20, characterBattleData.CurrentResistance);
            Assert.AreEqual(20, characterBattleData.CurrentEndurance);
            Assert.AreEqual(10, characterBattleData.CurrentPassion);
            Assert.AreEqual(20, characterBattleData.CurrentPersuasion);
            Assert.AreEqual(20, characterBattleData.CurrentWit);
        }

        [Test]
        public void ApplyStandardEffectUsePercentages()
        {
            var statusEffect = CreateStatusEffect(true, false, false, false, -20, 0, 0, 0, 0, 0, 0, 0);

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


            var activeEffect = new ActiveStatusEffect(statusEffect, 5);

            activeEffect.ApplyStandardEffect(characterBattleData);

            Assert.AreEqual(8, characterBattleData.CurrentResistance);
        }

        [Test]
        public void ApplyStandardEffectReduceStress()
        {
            var statusEffect = CreateStatusEffect(false, false, false, false, 0, 0, 0, 0, 0, -10, 0, 0);

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
            characterBattleData.IncreaseStress(10);

            var activeEffect = new ActiveStatusEffect(statusEffect, 5);

            activeEffect.ApplyStandardEffect(characterBattleData);

            Assert.AreEqual(0, characterBattleData.CurrentStress);
        }

        [Test]
        public void ApplyStandardEffectIncreaseStress()
        {
            var statusEffect = CreateStatusEffect(false, false, false, false, 0, 0, 0, 0, 0, 10, 0, 0);

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


            var activeEffect = new ActiveStatusEffect(statusEffect, 5);

            activeEffect.ApplyStandardEffect(characterBattleData);

            Assert.AreEqual(10, characterBattleData.CurrentStress);
        }

        [Test]
        public void ApplyStandardEffectReduceFocusPoints()
        {
            var statusEffect = CreateStatusEffect(false, false, false, false, 0, 0, 0, 0, 0, 0, -10, 0);

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

            var activeEffect = new ActiveStatusEffect(statusEffect, 5);

            activeEffect.ApplyStandardEffect(characterBattleData);

            Assert.AreEqual(0, characterBattleData.CurrentFocusPoints);
        }

        [Test]
        public void ApplyStandardEffectIncreaseFocusPoints()
        {
            var statusEffect = CreateStatusEffect(false, false, false, false, 0, 0, 0, 0, 0, 0, 10, 0);

            var character = new Character()
            {
                Resistance = 10,
                Endurance = 10,
                Passion = 10,
                Persuasion = 10,
                Wit = 10,
                FocusPointCapacity = 20
            };
            var characterBattleData = new CharacterBattleData();
            characterBattleData.InitializeCharacter(character);
            characterBattleData.DecreaseFocusPoints(10);


            var activeEffect = new ActiveStatusEffect(statusEffect, 5);

            activeEffect.ApplyStandardEffect(characterBattleData);

            Assert.AreEqual(20, characterBattleData.CurrentFocusPoints);
        }

        private StatusEffect CreateStatusEffect(bool usePercentages, bool isRecurring, bool adjustImmediately, bool doNotRestoreOnExpiration, int resistanceAdjustment, int enduranceAdjustment,
            int passionAdjustment, int persuasionAdjustment, int witAdjustment, int spAdjustment, int currentFpAdjustment, int fpCapacityAdjustment)
        {
            var newEffect = new StatusEffect()
            {
                Name = "TestEffect",
                UsePercentages = usePercentages,
                IsRecurring = isRecurring,
                AdjustImmediately = adjustImmediately,
                DoNotRestoreOnExpiration = doNotRestoreOnExpiration,
                ResistanceAdjustment = resistanceAdjustment,
                EnduranceAdjustment = enduranceAdjustment,
                PassionAdjustment = passionAdjustment,
                PersuasionAdjustment = persuasionAdjustment,
                WitAdjustment = witAdjustment,
                StressPointAdjustment = spAdjustment,
                CurrentFocusPointAdjustment = currentFpAdjustment,
                FocusPointCapacityAdjustment = fpCapacityAdjustment
            };

            return newEffect;
        }
    }
}
