using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BattleControllerTests : PlayTestBase
    {
        [UnityTest]
        public IEnumerator Start_UsingBattleDebugMenu_InstantiatesCombatants()
        {
            SetupBattleScene(true);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

            Assert.AreEqual(1, battleController.prosecutors.Count);
            Assert.AreEqual(1, battleController.defenseAttorneys.Count);
            Assert.IsNotNull(battleController.defendant);
        }

        [UnityTest]
        public IEnumerator Start_UsingBattleDebugMenu_InstantiatesCombatantsMappedFromDebugMenu()
        {
            var defenseAttorney = CreateCharacter(11, CharacterType.DefenseCharacter);
            var defenseAttorney2 = CreateCharacter(111, CharacterType.DefenseCharacter);
            var testDefendant = CreateCharacter(110, CharacterType.DefendantCharacter);
            var testCase = CreateCase(1, defenseAttorneys: new List<Character> {defenseAttorney, defenseAttorney2}, defendant: testDefendant);
            SetupBattleScene(true, testCase);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

            Assert.AreEqual(2, battleController.defenseAttorneys.Count);
            Assert.AreEqual("character 11", battleController.defenseAttorneys[0].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 110", battleController.defendant.GetComponent<CharacterBattleData>().displayName);
        }

        private void SetupBattleScene(bool useTestData, Case c = null) 
        {
            var debugMenuObj = new GameObject();
            var debugMenu = debugMenuObj.AddComponent<BattleDebugMenu>();
            if(useTestData) {
                var testCase = c == null ? CreateCase(0) : c;
                var party = new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter) };
                debugMenu.CaseData = testCase;
                debugMenu.EvidenceList = testCase.AllEvidence;
                debugMenu.StartingDefenseParty = testCase.DefenseAttorneys;
                debugMenu.TotalParty = party;
                debugMenu.StartingParty = party;
            }
            else {
                var battleData = new GameObject();
                AddToCleanup(battleData);
                battleData.tag = "BattleData";
            }
            AddToCleanup(debugMenuObj);
            
            var controllerObj = new GameObject("BattleController");
            controllerObj.AddComponent<BattleData>();
            var controller = controllerObj.AddComponent<BattleController>();
            controller.DebugMenu = debugMenu;
            controller.ProsecutionPlaceholders = new List<GameObject> {new GameObject(), new GameObject()};
            controller.DefendantPlaceholder = new GameObject();
            controller.DefensePlaceholders = new List<GameObject> {new GameObject(), new GameObject()};

            AddToCleanup(controllerObj);
        }
    }
}
