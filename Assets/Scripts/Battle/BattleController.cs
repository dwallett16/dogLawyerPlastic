using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleController : MonoBehaviour
{
    public BattleDebugMenu DebugMenu;
    public List<GameObject> ProsecutionPlaceholders;
    public List<GameObject> DefensePlaceholders;
    public GameObject DefendantPlaceholder;
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
        MapBattleData();
        InstantiateCombatants();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void MapBattleData()
    {
        if (isUsingTestData)
        {
            battleData.CaseData = DebugMenu.CaseData;
            battleData.EvidenceList = DebugMenu.EvidenceList;
            battleData.TotalParty = DebugMenu.TotalParty;
            battleData.StartingParty = DebugMenu.StartingParty;
            battleData.StartingDefenseParty = DebugMenu.StartingDefenseParty;
            battleData.TotalDefenseParty = DebugMenu.CaseData.DefenseAttorneys;
            battleData.Defendant = DebugMenu.CaseData.Defendant;
        }
        else
        {
            throw new System.Exception("Non-debug battle data not yet implemented");
        }
    }

    private Dictionary<GameObject, Character> GetBattlePrefabsFromCharacters(List<Character> characters) {
        var battlePrefabDictionary = new Dictionary<GameObject, Character>();
        foreach(var character in characters) {
            battlePrefabDictionary.Add(character.BattlePrefab, character);
        }
        return battlePrefabDictionary;
    }

    private void InstantiateCombatants() {
        var placeholderIndex = 0;
        foreach (var prosecutor in battleData.StartingParty) {
            //key is prefab, value is scriptableObject
            var prosecutorInstance = Instantiate(prosecutor.BattlePrefab, ProsecutionPlaceholders[placeholderIndex].transform.position, ProsecutionPlaceholders[placeholderIndex].transform.rotation);
            var prosecutorData = prosecutorInstance.GetComponent<CharacterBattleData>();
            prosecutorData.MapFromScriptableObject(prosecutor);
            prosecutors.Add(prosecutorInstance);
            placeholderIndex++;
        }
        placeholderIndex = 0;
        foreach (var defenseAttorney in battleData.StartingDefenseParty) {
            var defenseInstance = Instantiate(defenseAttorney.BattlePrefab, DefensePlaceholders[placeholderIndex].transform.position, DefensePlaceholders[placeholderIndex].transform.rotation);
            var defenseData = defenseInstance.GetComponent<CharacterBattleData>();
            defenseData.MapFromScriptableObject(defenseAttorney);
            defenseAttorneys.Add(defenseInstance);
            placeholderIndex++;
        }
        var defendantInstance = Instantiate(battleData.Defendant.BattlePrefab, DefendantPlaceholder.transform.position, DefendantPlaceholder.transform.rotation);
        var defendantData = defendantInstance.GetComponent<CharacterBattleData>();
        defendantData.MapFromScriptableObject(battleData.Defendant);
        defendant = defendantInstance;
    }
}
