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
            juryData.AddPoints(35);

            Assert.AreEqual(3, juryData.LockedInJurors);
        }

        [Test]
        public void RemovePointsNegativeValueWontSubtractPointsIfJurorLocked()
        {
            var juryData = new JuryData(5, 10);
            juryData.AddPoints(32);

            juryData.RemovePoints(-10);

            Assert.AreEqual(30, juryData.CurrentPoints);
        }

    }
}
