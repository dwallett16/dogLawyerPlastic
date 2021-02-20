using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Battle
{
    public class BattleControllerTests : PlayTestBase
    {
        [UnityTest]
        public IEnumerator StartWithBattleDebugMenuInstantiatesCombatants()
        {
            SetupBattleScene(true);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

            Assert.AreEqual(1, battleController.prosecutors.Count);
            Assert.AreEqual(1, battleController.defenseAttorneys.Count);
            Assert.IsNotNull(battleController.defendant);
        }

        [UnityTest]
        public IEnumerator StartWithBattleDebugMenuInstantiatesOpponentsMappedFromDebugMenu()
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
            Assert.AreEqual(CharacterType.DefenseCharacter, battleController.defenseAttorneys[0].GetComponent<CharacterBattleData>().type);
            Assert.AreEqual("character 110", battleController.defendant.GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.DefendantCharacter, battleController.defendant.GetComponent<CharacterBattleData>().type);
        }

        [UnityTest]
        public IEnumerator StartWithBattleDebugMenuInstantiatesProsecutionMappedFromDebugMenu()
        {
            SetupBattleScene(true);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

            Assert.AreEqual(1, battleController.prosecutors.Count);
            Assert.AreEqual("character 10", battleController.prosecutors[0].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.PlayerCharacter, battleController.prosecutors[0].GetComponent<CharacterBattleData>().type);
        }

        [UnityTest]
        public IEnumerator StartDeterminesCombatantOrderBasedOnWit()
        {
            var defenseAttorney = CreateCharacter(11, CharacterType.DefenseCharacter, wit: 2);
            var defenseAttorney2 = CreateCharacter(111, CharacterType.DefenseCharacter, wit: 3);
            var testDefendant = CreateCharacter(110, CharacterType.DefendantCharacter);
            var testCase = CreateCase(1, defenseAttorneys: new List<Character> {defenseAttorney, defenseAttorney2}, defendant: testDefendant);
            var party = new List<Character> {CreateCharacter(0, CharacterType.PlayerCharacter, wit: 1), CreateCharacter(1, CharacterType.PlayerCharacter, wit: 4) };
            SetupBattleScene(true, testCase, party);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            var combatants = battleController.allCombatants.ToArray();

            Assert.AreEqual("character 1", combatants[0].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 111", combatants[1].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 11", combatants[2].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 0", combatants[3].GetComponent<CharacterBattleData>().displayName);
        }

        private void SetupBattleScene(bool useTestData, Case c = null, List<Character> testParty = null) 
        {
            var debugMenuObj = new GameObject();
            var debugMenu = debugMenuObj.AddComponent<BattleDebugMenu>();
            if(useTestData) {
                var testCase = c == null ? CreateCase(0) : c;
                var party = testParty == null ? new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter) } : testParty;
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
