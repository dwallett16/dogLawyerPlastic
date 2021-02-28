
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace Battle {
    public class PlayerActionSelectStateTests {

        [Test]
        public void ExecuteReturnsBattleState() {
            var state = new PlayerActionSelectState();
            var controller = new BattleController();

            var result = state.Execute(controller);

            Assert.NotNull(result);
        }
    }
}
