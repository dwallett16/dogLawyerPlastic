using NUnit.Framework;
using UnityEngine;

namespace Battle {
    public class RestActionTests
    {
        [Test]
        public void ActReplenishesFocusPointsToCurrentTurnCharacter() {
            var restAction = new RestAction();
            var character = new GameObject();
            character.AddComponent<CharacterBattleData>().FocusPointCapacity = 100;
            character.GetComponent<CharacterBattleData>().IncreaseFocusPoints(10);
            var actionData = new ActionData {
                CurrentCombatant = character
            };

            restAction.Act(actionData);

            Assert.IsTrue(actionData.CurrentCombatant.GetComponent<CharacterBattleData>().CurrentFocusPoints > 10);
        }
    }
}
