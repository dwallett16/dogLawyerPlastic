using NUnit.Framework;
using UnityEngine;

namespace Battle {
    public class RestActionTests
    {
        [Test]
        public void ActReplenishesFocusPointsToCurrentTurnCharacter() {
            var restAction = new RestAction();
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>().currentFocusPoints = 10;
            var actionData = new ActionData {
                CurrentCombatant = character
            };

            restAction.Act(actionData);

            Assert.IsTrue(actionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints > 10);
        }
    }
}
