using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Battle
{
    public class JuryDataTests
    {
        [Test]
        public void GetLockedInJurorsWithJuryPointsReturnsNumberOfLockedInJurors()
        {
            var juryData = new JuryData(5, 10);
            juryData.ChangePoints(35);

            Assert.AreEqual(3, juryData.LockedInJurors);
        }

        [Test]
        public void ChangePointsWontAddMoreThanTotal()
        {
            var juryData = new JuryData(5, 10);
            juryData.ChangePoints(60);

            Assert.AreEqual(50, juryData.CurrentPoints);
        }

        [Test]
        public void ChangePointsWontSubtractLessThanZero()
        {
            var juryData = new JuryData(5, 10);
            juryData.ChangePoints(-60);

            Assert.AreEqual(0, juryData.CurrentPoints);
        }

        [Test]
        public void ChangePointsAddsPoints()
        {
            var juryData = new JuryData(5, 10);
            juryData.ChangePoints(40);

            Assert.AreEqual(40, juryData.CurrentPoints);
        }

    }
}
