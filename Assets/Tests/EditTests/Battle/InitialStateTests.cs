using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Assets.Scripts.Battle.States;

namespace Battle{
    public class InitialStateTests {
        
        [Test]
        public void ExecuteReturnsNextTurnState() {
            var state = new InitialState();
            var controller = new BattleController();
            controller.NextTurn = new NextTurnState();

            var result = state.Execute(controller);

            Assert.IsInstanceOf<NextTurnState>(result);
        }
    }
}
