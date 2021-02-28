
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
            controller.ActionData = new ActionData();

            var result = state.Execute(controller);

            Assert.NotNull(result);
        }

        [Test]
        public void ExecuteReturnsPlayerActionStateIfRestButtonPressed() {
            var state = new PlayerActionSelectState();
            var controller = new BattleController();
            controller.ActionData = new ActionData();
            controller.PlayerAction = new PlayerActionState();
            controller.ActionData.ButtonAction = "Rest";

            var result = state.Execute(controller);

            Assert.IsInstanceOf<PlayerActionState>(result);
        }
    }
}
