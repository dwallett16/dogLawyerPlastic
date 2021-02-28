using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Assets.Scripts.Battle.States;

namespace Battle {
    public class PlayerActionStateTests
    {
        [Test]
        public void ExecuteReturnsNextTurnState() {
            var playerActionState = new PlayerActionState();
            var controller = new BattleController();
            controller.ActionData = new ActionData();
            controller.NextTurn = new NextTurnState();

            var result = playerActionState.Execute(controller);

            Assert.IsInstanceOf<NextTurnState>(result);
        }

        [Test]
        public void ExecuteReplenishesFocusPointsToCurrentTurnCharacterWhenButtonActionRest() {
            var playerActionState = new PlayerActionState();
            var controller = new BattleController();
            controller.ActionData = new ActionData {
                ButtonAction = "Rest"
            };
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>().currentFocusPoints = 10;
            controller.CurrentCombatant = character;

            playerActionState.Execute(controller);

            Assert.IsTrue(controller.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints > 10);
        }

    }
}
