using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Battle.States;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;
using UnityEngine.UI;

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
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

            yield return new WaitUntil(() => battleController.CurrentState == battleController.Initial);
            
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
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            
            yield return new WaitUntil(() => battleController.CurrentState == battleController.Initial);
            
            Assert.IsInstanceOf<NextTurnState>(battleController.CurrentState);
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

        [UnityTest]
        public IEnumerator PlayerButtonActionRestRestoresFocusPointsToCurrentCombatant()
        {
            var party = new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter, wit: 1000) };
            SetupBattleScene(true, testParty: party);

            yield return new WaitForSeconds(0.2f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            var oldFp = battleController.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().currentFocusPoints;
            battleController.SetButtonAction(Constants.Rest);
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(battleController.Prosecutors[0].GetComponent<CharacterBattleData>().currentFocusPoints > oldFp);
            Assert.AreEqual(battleController.ActionData.CurrentCombatant, battleController.DefenseAttorneys[0]);
        }

        [UnityTest]
        public IEnumerator PlayerButtonActionSkillsPopulatesSkillsMenuWithCurrentCombatantSkills()
        {
            var playerSkills = new List<Skill> {CreateSkill(0), CreateSkill(1)};
            var party = new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter, skills: playerSkills, wit: 1000) };
            SetupBattleScene(true, testParty: party);

            yield return new WaitForSeconds(0.1f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            battleController.SetButtonAction(Constants.Skills);
            yield return new WaitForSeconds(0.3f);
            var skillPanel = GameObject.Find("SkillsPanel");
            var skills = new List<GameObject>();

            foreach(Transform c in skillPanel.transform) {
                if(c.gameObject.tag == Constants.SkillButton && c.gameObject.activeInHierarchy)
                    skills.Add(c.gameObject);
            }

            Assert.AreEqual(battleController.ActionData.CurrentCombatant.GetComponent<CharacterBattleData>().skills.Count, skills.Count);
        }

        [UnityTest]
        public IEnumerator PlayerButtonActionSkillsOffenseSkillSelectsBetweenDefenseAttorneys()
        {
            var offenseSkill = CreateSkill(0);
            var playerSkills = new List<Skill> { offenseSkill };
            var party = new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter, skills: playerSkills, wit: 1000) };
            SetupBattleScene(true, testParty: party);

            yield return new WaitForSeconds(0.1f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            battleController.SetButtonAction(Constants.Skills);
            yield return new WaitForSeconds(0.3f);
            var selectedSkillData = new SkillButtonData
            {
                SkillData = offenseSkill
            };
            battleController.SetActionDataSkill(selectedSkillData);
            battleController.PlayerTargetSelect.NewState = true;
            battleController.CurrentState = battleController.PlayerTargetSelect;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual("character 1", battleController.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.DefenseCharacter, battleController.ActionData.Target.GetComponent<CharacterBattleData>().type);
        }

        [UnityTest]
        public IEnumerator PlayerButtonActionSkillsSupportSkillSelectsBetweenProsecutors()
        {
            var supportSkill = CreateSkill(0, SkillTarget.Prosecutors);
            var playerSkills = new List<Skill> { supportSkill };
            var party = new List<Character> { CreateCharacter(10, CharacterType.PlayerCharacter, skills: playerSkills, wit: 1000) };
            SetupBattleScene(true, testParty: party);

            yield return new WaitForSeconds(0.1f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            battleController.SetButtonAction(Constants.Skills);
            yield return new WaitForSeconds(0.3f);
            var selectedSkillData = new SkillButtonData
            {
                SkillData = supportSkill
            };
            battleController.SetActionDataSkill(selectedSkillData);
            battleController.PlayerTargetSelect.NewState = true;
            battleController.CurrentState = battleController.PlayerTargetSelect;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual("character 10", battleController.ActionData.Target.GetComponent<CharacterBattleData>().displayName);
            Assert.AreEqual(CharacterType.PlayerCharacter, battleController.ActionData.Target.GetComponent<CharacterBattleData>().type);
        }

        [UnityTest]
        public IEnumerator PlayerButtonActionEvidencePopulatesEvidencePanel()
        {
            SetupBattleScene(true);

            yield return new WaitForSeconds(1f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            battleController.SetButtonAction(Constants.Evidence);
            yield return new WaitForSeconds(0.3f);
            
            var evidencePanel = GameObject.Find("EvidencePanel");
            var evidenceButtons = new List<GameObject>();
            foreach (Transform c in evidencePanel.transform)
            {
                if (c.gameObject.activeInHierarchy)
                    evidenceButtons.Add(c.gameObject);
            }

            Assert.AreEqual(3, evidenceButtons.Count);
            Assert.AreEqual("evidence 0", evidenceButtons[0].GetComponent<EvidenceButtonData>().EvidenceData.Name);
            Assert.AreEqual("evidence 1", evidenceButtons[1].GetComponent<EvidenceButtonData>().EvidenceData.Name);
            Assert.AreEqual("evidence 2", evidenceButtons[2].GetComponent<EvidenceButtonData>().EvidenceData.Name);
        }

        [UnityTest]
        public IEnumerator PlayerButtonActionEvidencePresentingEvidenceAffectsJury()
        {
            SetupBattleScene(true);

            yield return new WaitForSeconds(1f);
            var battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
            battleController.SetButtonAction(Constants.Evidence);
            yield return new WaitForSeconds(0.3f);

            var evidencePanel = GameObject.Find("EvidencePanel");
            var evidenceButtons = new List<GameObject>();
            foreach (Transform c in evidencePanel.transform)
            {
                if (c.gameObject.activeInHierarchy)
                    evidenceButtons.Add(c.gameObject);
            }
            battleController.SetActionDataEvidence(new EvidenceButtonData { EvidenceData = CreateEvidence(0, CreateCase(0)) });
            yield return new WaitUntil(() => battleController.CurrentState == battleController.PlayerEvidenceSelect);
            var inputMock = Substitute.For<IInputManager>();
            inputMock.GetButtonDown(Constants.Submit).Returns(true);
            battleController.InputManager = inputMock;
            battleController.MenuConfirmSelection = true;
            yield return new WaitForSeconds(0.3f);
            var jury = GameObject.Find("Jury");

            Assert.True(jury.GetComponent<JuryController>().GetJuryPoints() > 0);
        }

        private void SetupBattleScene(bool useTestData, Case c = null, List<Character> testParty = null, string buttonAction = "") 
        {
            //Debug Menu
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
            
            //Battle Controller
            var controllerObj = new GameObject("BattleController");
            AddToCleanup(controllerObj);
            controllerObj.AddComponent<BattleData>();
            var controller = controllerObj.AddComponent<BattleController>();
            controller.DebugMenu = debugMenu;
            controller.ProsecutionPlaceholders = new List<GameObject> {new GameObject(), new GameObject()};
            controller.DefendantPlaceholder = new GameObject();
            controller.DefensePlaceholders = new List<GameObject> {new GameObject(), new GameObject()};
            controller.ActionData = new ActionData {
                ButtonAction = buttonAction
            };

            //Action Buttons
            var actionButtonPanel = new GameObject("ActionPanel");
            controller.ActionButtonPanel = actionButtonPanel;
            AddToCleanup(actionButtonPanel);

            //Skills Panel
            var skillsPanel = new GameObject("SkillsPanel");
            AddToCleanup(skillsPanel);
            controller.SkillPanel = skillsPanel;
            controller.SkillButtons = new List<GameObject>();
            for (int i = 0; i < 4; i++)
            {
                var skillButton = new GameObject();
                skillButton.AddComponent<SkillButtonData>();
                skillButton.tag = Constants.SkillButton;

                var textObject = new GameObject();
                textObject.AddComponent<Text>();
                textObject.transform.SetParent(skillButton.transform);

                controller.SkillButtons.Add(skillButton);
                controller.SkillButtons[i].transform.SetParent(skillsPanel.transform);
            }
            controller.TargetSelector = new GameObject();
            AddToCleanup(controller.TargetSelector);

            //Evidence Panel
            var evidencePanel = new GameObject("EvidencePanel");
            AddToCleanup(evidencePanel);
            controller.EvidencePanel = evidencePanel;
            controller.EvidenceButtons = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                var evidenceButton = new GameObject();
                evidenceButton.AddComponent<EvidenceButtonData>();

                var textObject = new GameObject();
                textObject.AddComponent<Text>();
                textObject.transform.SetParent(evidenceButton.transform);

                controller.EvidenceButtons.Add(evidenceButton);
                controller.EvidenceButtons[i].transform.SetParent(evidencePanel.transform);
            }
            var evidenceConfirmPanel = new GameObject();
            AddToCleanup(evidenceConfirmPanel);
            controller.EvidenceConfirmPanel = evidenceConfirmPanel;
            controller.EvidencePanel.SetActive(false);

            //Jury
            var juryObject = new GameObject("Jury");
            juryObject.AddComponent<JuryController>();
            juryObject.GetComponent<JuryController>().NumberOfJurors = 5;
            juryObject.GetComponent<JuryController>().PointsPerJuror = 10;
            AddToCleanup(juryObject);
            controller.Jury = juryObject;
        }
    }
}
