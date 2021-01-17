using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public BattleDebugMenu DebugMenu;
    public List<GameObject> ProsecutionPlaceholders;
    public List<GameObject> DefensePlaceholders;
    public GameObject GenericCombatant;
    private List<GameObject> prosecutors;
    private List<GameObject> defenseAttorneys;

    // Start is called before the first frame update
    void Start()
    {
        prosecutors = new List<GameObject>();
        defenseAttorneys = new List<GameObject>();
        if(DebugMenu.UseTestData) {
            Debug.Log("using test data");
            InstantiateCombatants();
            MapFromScriptableObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateCombatants() {
        foreach(var prosecutorPlaceholder in ProsecutionPlaceholders) {
            var prosecutorInstance = Instantiate(GenericCombatant, prosecutorPlaceholder.transform);
            prosecutors.Add(prosecutorInstance);
        }
        foreach(var defensePlaceholder in defenseAttorneys) {
            var defenseAttorneyInstance = Instantiate(GenericCombatant, defensePlaceholder.transform);
            defenseAttorneys.Add(defenseAttorneyInstance);
        }
    }

    private void MapFromScriptableObject()
    {
        foreach(var partyMember in DebugMenu.StartingParty) {

        }
    }
}
