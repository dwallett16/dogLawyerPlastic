using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Battle.States;

public class BattleController : MonoBehaviour
{
    public BattleDebugMenu DebugMenu;
    public List<GameObject> ProsecutionPlaceholders;
    public List<GameObject> DefensePlaceholders;
    public GameObject DefendantPlaceholder;
    public List<GameObject> Prosecutors;
    public List<GameObject> DefenseAttorneys;
    public Queue<GameObject> AllCombatants;
    public GameObject Defendant;
    public IBattleState CurrentState;
    public ActionData ActionData;
    private BattleData battleData;
    private bool isUsingTestData;

    //states
    public PlayerActionSelectState PlayerActionSelect;
    public InitialState Initial;
    public EnemyActionSelectState EnemyActionSelect;
    public ActionState Action;
    public NextTurnState NextTurn;

    // Start is called before the first frame update
    void Start()
    {
        isUsingTestData = GameObject.FindGameObjectWithTag("BattleData") == null;
        Prosecutors = new List<GameObject>();
        DefenseAttorneys = new List<GameObject>();
        battleData = GetComponent<BattleData>();
        AllCombatants = new Queue<GameObject>();
        MapBattleData();
        InstantiateCombatants();
        OrderCombatants();

        ActionData = new ActionData();
        PlayerActionSelect = new PlayerActionSelectState();
        Initial = new InitialState();
        EnemyActionSelect = new EnemyActionSelectState();
        Action = new ActionState();
        NextTurn = new NextTurnState();

        CurrentState = Initial;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState = CurrentState.Execute(this);
    }

    public void SetButtonAction(string action) {
        ActionData.ButtonAction = action;
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

    private void InstantiateCombatants() {
        var placeholderIndex = 0;
        foreach (var prosecutor in battleData.StartingParty) {
            var prosecutorInstance = Instantiate(prosecutor.BattlePrefab, ProsecutionPlaceholders[placeholderIndex].transform.position, ProsecutionPlaceholders[placeholderIndex].transform.rotation);
            var prosecutorData = prosecutorInstance.GetComponent<CharacterBattleData>();
            prosecutorData.MapFromScriptableObject(prosecutor);
            Prosecutors.Add(prosecutorInstance);
            placeholderIndex++;
        }
        placeholderIndex = 0;
        foreach (var defenseAttorney in battleData.StartingDefenseParty) {
            var defenseInstance = Instantiate(defenseAttorney.BattlePrefab, DefensePlaceholders[placeholderIndex].transform.position, DefensePlaceholders[placeholderIndex].transform.rotation);
            var defenseData = defenseInstance.GetComponent<CharacterBattleData>();
            defenseData.MapFromScriptableObject(defenseAttorney);
            DefenseAttorneys.Add(defenseInstance);
            placeholderIndex++;
        }
        var defendantInstance = Instantiate(battleData.Defendant.BattlePrefab, DefendantPlaceholder.transform.position, DefendantPlaceholder.transform.rotation);
        var defendantData = defendantInstance.GetComponent<CharacterBattleData>();
        defendantData.MapFromScriptableObject(battleData.Defendant);
        Defendant = defendantInstance;
    }

    private void OrderCombatants()
    {
        List<GameObject> combatantList = new List<GameObject>();
        combatantList.AddRange(Prosecutors);
        combatantList.AddRange(DefenseAttorneys);
        IEnumerable<GameObject> query = combatantList.OrderByDescending(gameObject => gameObject.GetComponent<CharacterBattleData>().wit);

        foreach (GameObject combatant in query)
        {
            AllCombatants.Enqueue(combatant);
        }
    }
}
