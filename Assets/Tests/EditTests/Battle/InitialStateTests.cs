using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Battle{
    public class InitialStateTests {
        
        [Test]
        public void ExecuteReturnsPlayerActionSelectState() {
            var state = new InitialState();
            var controller = new BattleController();
            controller.PlayerActionSelect = new PlayerActionSelectState();

            var result = state.Execute(controller);

            Assert.IsInstanceOf<PlayerActionSelectState>(result);
        }

    }
}
