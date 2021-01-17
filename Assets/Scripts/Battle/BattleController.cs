using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public BattleDebugMenu DebugMenu;
    public List<GameObject> ProsecutionPlaceholders;
    public List<GameObject> DefensePlaceholders;
    public GameObject DefendantPlaceholder;
    public GameObject GenericCombatant;
    private List<GameObject> prosecutors;
    private List<GameObject> defenseAttorneys;
    private GameObject defendant;
    private BattleData battleData;

    private bool isUsingTestData;

    // Start is called before the first frame update
    void Start()
    {
        isUsingTestData = GameObject.FindGameObjectWithTag("BattleData") == null;
        prosecutors = new List<GameObject>();
        defenseAttorneys = new List<GameObject>();
        battleData = GetComponent<BattleData>();
        InstantiateCombatants();
        MapFromBattleData();
        MapFromScriptableObject();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateCombatants() {
        foreach(var prosecutorPlaceholder in ProsecutionPlaceholders) {
            var prosecutorInstance = Instantiate(GenericCombatant, prosecutorPlaceholder.transform.position, prosecutorPlaceholder.transform.rotation);
            prosecutors.Add(prosecutorInstance);
        }
        foreach(var defensePlaceholder in DefensePlaceholders) {
            var defenseAttorneyInstance = Instantiate(GenericCombatant, defensePlaceholder.transform.position, defensePlaceholder.transform.rotation);
            defenseAttorneys.Add(defenseAttorneyInstance);
        }
        var defendant = Instantiate(GenericCombatant, DefendantPlaceholder.transform.position, DefendantPlaceholder.transform.rotation);
    }

    private void MapFromScriptableObject()
    {
        //Map Prosecutors
        for (int i = 0; i < battleData.StartingParty.Count; i++)
        {
            //Map Data
            var characterBattleData = prosecutors[i].GetComponent<CharacterBattleData>();
            characterBattleData.displayName = battleData.StartingParty[i].Name;
            characterBattleData.type = battleData.StartingParty[i].Type;
            characterBattleData.personality = battleData.StartingParty[i].Personality;
            characterBattleData.stressCapacity = battleData.StartingParty[i].StressCapacity;
            characterBattleData.focusPointCapacity = battleData.StartingParty[i].FocusPointCapacity;
            characterBattleData.wit = battleData.StartingParty[i].Wit;
            characterBattleData.resistance = battleData.StartingParty[i].Resistance;
            characterBattleData.endurance = battleData.StartingParty[i].Endurance;
            characterBattleData.passion = battleData.StartingParty[i].Passion;
            characterBattleData.persuasion = battleData.StartingParty[i].Persuasion;

            //Map Graphics
            var meshRenderer = prosecutors[i].GetComponent<MeshRenderer>();
            meshRenderer.material = battleData.StartingParty[i].Material;
            var skeletonAnimation = prosecutors[i].GetComponent<SkeletonAnimation>();
            skeletonAnimation.skeletonDataAsset = battleData.StartingParty[i].SkeletonData;
            skeletonAnimation.Initialize(true);
        }

        //Map Defense Attorneys
        for (int i = 0; i < battleData.StartingDefenseParty.Count; i++)
        {
            //Map Data
            var characterBattleData = defenseAttorneys[i].GetComponent<CharacterBattleData>();
            characterBattleData.displayName = battleData.StartingDefenseParty[i].Name;
            characterBattleData.type = battleData.StartingDefenseParty[i].Type;
            characterBattleData.personality = battleData.StartingDefenseParty[i].Personality;
            characterBattleData.stressCapacity = battleData.StartingDefenseParty[i].StressCapacity;
            characterBattleData.focusPointCapacity = battleData.StartingDefenseParty[i].FocusPointCapacity;
            characterBattleData.wit = battleData.StartingDefenseParty[i].Wit;
            characterBattleData.resistance = battleData.StartingDefenseParty[i].Resistance;
            characterBattleData.endurance = battleData.StartingDefenseParty[i].Endurance;
            characterBattleData.passion = battleData.StartingDefenseParty[i].Passion;
            characterBattleData.persuasion = battleData.StartingDefenseParty[i].Persuasion;
            characterBattleData.specialty = battleData.StartingDefenseParty[i].Specialty;
            characterBattleData.skills = battleData.StartingDefenseParty[i].Skills;

            //Map Graphics
            var meshRenderer = prosecutors[i].GetComponent<MeshRenderer>();
            meshRenderer.material = battleData.StartingDefenseParty[i].Material;
            var skeletonAnimation = prosecutors[i].GetComponent<SkeletonAnimation>();
            skeletonAnimation.skeletonDataAsset = battleData.StartingDefenseParty[i].SkeletonData;
            skeletonAnimation.Initialize(true);
        }

        //Map Defendant
        //Map Data
        var characterBattleDataDefendant = defendant.GetComponent<CharacterBattleData>();
        characterBattleDataDefendant.displayName = battleData.CaseData.Defendant.Name;
        characterBattleDataDefendant.stressCapacity = battleData.CaseData.Defendant.StressCapacity;
        characterBattleDataDefendant.resistance = battleData.CaseData.Defendant.Resistance;
        characterBattleDataDefendant.endurance = battleData.CaseData.Defendant.Endurance;

        //Map Graphics
        var meshRendererDefendant = defendant.GetComponent<MeshRenderer>();
        meshRendererDefendant.material = battleData.CaseData.Defendant.Material;
        var skeletonAnimationDefendant = defendant.GetComponent<SkeletonAnimation>();
        skeletonAnimationDefendant.skeletonDataAsset = battleData.CaseData.Defendant.SkeletonData;
        skeletonAnimationDefendant.Initialize(true);
    }

    private void MapFromBattleData()
    {
        if (isUsingTestData)
        {
            battleData.CaseData = DebugMenu.CaseData;
            battleData.TotalParty = DebugMenu.TotalParty;
            battleData.StartingParty = DebugMenu.StartingParty;
            battleData.StartingDefenseParty = DebugMenu.StartingDefenseParty;
            battleData.EvidenceList = DebugMenu.EvidenceList;
        }
        else
        {
            throw new System.Exception("Non-debug battle data not yet implemented");
        }
    }
}
