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

            Assert.AreEqual(1, battleController.Prosecutors.Count);
            Assert.AreEqual(1, battleController.DefenseAttorneys.Count);
            Assert.IsNotNull(battleController.Defendant);
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

            Assert.AreEqual(2, battleController.DefenseAttorneys.Count);
            Assert.AreEqual("character 11", battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.DefenseCharacter, battleController.DefenseAttorneys[0].GetComponent<CharacterBattleData>().type);
            Assert.AreEqual("character 110", battleController.Defendant.GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.DefendantCharacter, battleController.Defendant.GetComponent<CharacterBattleData>().type);
        }

        [UnityTest]
        public IEnumerator StartWithBattleDebugMenuInstantiatesProsecutionMappedFromDebugMenu()
        {
            SetupBattleScene(true);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

            Assert.AreEqual(1, battleController.Prosecutors.Count);
            Assert.AreEqual("character 10", battleController.Prosecutors[0].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.PlayerCharacter, battleController.Prosecutors[0].GetComponent<CharacterBattleData>().type);
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
            var combatants = battleController.AllCombatants.ToArray();

            Assert.AreEqual("character 1", combatants[0].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 111", combatants[1].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 11", combatants[2].GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual("character 0", combatants[3].GetComponent<CharacterBattleData>().displayName);
        }

        [UnityTest]
        public IEnumerator StartSetsInitialState()
        {
            SetupBattleScene(true);

            yield return new WaitForFixedUpdate();
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            
            Assert.NotNull(battleController.CurrentState);
        }

        [UnityTest]
        public IEnumerator UpdateSetsCurrentCombatantToFirstInQueue()
        {
            var party = new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter, wit: 1000) };
            SetupBattleScene(true, testParty: party);

            yield return new WaitForSeconds(0.1f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            
            Assert.AreEqual(battleController.ActionData.CurrentCombatant, battleController.Prosecutors[0]);
        }

        private void SetupBattleScene(bool useTestData, Case c = null, List<Character> testParty = null, string buttonAction = "") 
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
            controller.ActionData = new ActionData {
                ButtonAction = buttonAction
            };

            AddToCleanup(controllerObj);
        }
    }
}
