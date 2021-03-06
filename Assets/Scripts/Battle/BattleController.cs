using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Battle.States;
using System;
using Assets.Scripts.Data.ScriptableObjects.StatusEffectData;
using Assets.Scripts.Battle.Utilities;

public class BattleController : MonoBehaviour
{
    public BattleDebugMenu DebugMenu;
    public List<GameObject> ProsecutionPlaceholders;
    public List<GameObject> DefensePlaceholders;
    public GameObject DefendantPlaceholder;
    [NonSerialized]
    public List<GameObject> Prosecutors;
    [NonSerialized]
    public List<GameObject> DefenseAttorneys;
    public GameObject Jury;
    public Queue<GameObject> AllCombatants;
    [NonSerialized]
    public GameObject Defendant;
    public BattleState CurrentState;
    public ActionData ActionData;
    public BattleData battleData;
    private bool isUsingTestData;
    [NonSerialized]
    public List<GameObject> TargetList;
    public int EffectiveEvidenceCount;
    public StatusEffect StunnedEffect;

    //Inputs
    public IInputManager InputManager;
    public bool IsBackButtonPressed;
    public bool IsSubmitButtonPressed;
    public float HorizontalAxis;
    public bool MenuConfirmSelection;

    //UI
    public List<GameObject> SkillButtons;
    public GameObject SkillPanel;
    public GameObject EvidencePanel;
    public List<GameObject> EvidenceButtons;
    public GameObject ActionButtonPanel;
    public GameObject EvidenceConfirmPanel;
    public GameObject EvidenceConfirmButton;
    public GameObject TargetSelector;

    //states
    public PlayerActionSelectState PlayerActionSelect;
    public InitialState Initial;
    public EnemyActionSelectState EnemyActionSelect;
    public ActionState Action;
    public NextTurnState NextTurn;
    public PlayerSkillSelectState PlayerSkillSelect;
    public PlayerTargetSelectState PlayerTargetSelect;
    public PlayerEvidenceSelectState PlayerEvidenceSelect;
    public EndTurnState EndTurn;

    // Start is called before the first frame update
    void Start()
    {
        isUsingTestData = GameObject.FindGameObjectWithTag("BattleData") == null;
        Prosecutors = new List<GameObject>();
        DefenseAttorneys = new List<GameObject>();
        battleData = GetComponent<BattleData>();
        AllCombatants = new Queue<GameObject>();
        TargetList = new List<GameObject>();
        InputManager = new InputManager();

        MapBattleData();
        InstantiateCombatants();
        OrderCombatants();

        ActionData = new ActionData();
        PlayerActionSelect = new PlayerActionSelectState();
        Initial = new InitialState();
        EnemyActionSelect = new EnemyActionSelectState(new ProbabilityHelper(), new AiUtilities());
        Action = new ActionState();
        NextTurn = new NextTurnState();
        PlayerSkillSelect = new PlayerSkillSelectState();
        PlayerTargetSelect = new PlayerTargetSelectState();
        PlayerEvidenceSelect = new PlayerEvidenceSelectState();
        EndTurn = new EndTurnState();

        CurrentState = Initial;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
        CurrentState = CurrentState.Execute(this);
    }

    public void SetButtonAction(string action) {
        ActionData.ButtonAction = action;
    }

    public void SetActionDataSkill(SkillButtonData skillButtonData)
    {
        ActionData.SelectedSkill = skillButtonData.SkillData;
    }

    public void SetActionDataEvidence(EvidenceButtonData evidenceButtonData)
    {
        ActionData.SelectedEvidence = evidenceButtonData.EvidenceData;
    }

    public void SetMenuConfirmSelection(bool isConfirmed)
    {
        MenuConfirmSelection = isConfirmed;
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
            prosecutorData.InitializeCharacter(prosecutor);
            Prosecutors.Add(prosecutorInstance);
            placeholderIndex++;
        }
        placeholderIndex = 0;
        foreach (var defenseAttorney in battleData.StartingDefenseParty) {
            var defenseInstance = Instantiate(defenseAttorney.BattlePrefab, DefensePlaceholders[placeholderIndex].transform.position, DefensePlaceholders[placeholderIndex].transform.rotation);
            var defenseData = defenseInstance.GetComponent<CharacterBattleData>();
            defenseData.InitializeCharacter(defenseAttorney);
            DefenseAttorneys.Add(defenseInstance);
            placeholderIndex++;
        }
        var defendantInstance = Instantiate(battleData.Defendant.BattlePrefab, DefendantPlaceholder.transform.position, DefendantPlaceholder.transform.rotation);
        var defendantData = defendantInstance.GetComponent<CharacterBattleData>();
        defendantData.InitializeCharacter(battleData.Defendant);
        Defendant = defendantInstance;
    }

    private void OrderCombatants()
    {
        List<GameObject> combatantList = new List<GameObject>();
        combatantList.AddRange(Prosecutors);
        combatantList.AddRange(DefenseAttorneys);
        IEnumerable<GameObject> query = combatantList.OrderByDescending(gameObject => gameObject.GetComponent<CharacterBattleData>().Wit);

        foreach (GameObject combatant in query)
        {
            AllCombatants.Enqueue(combatant);
        }
    }

    private void CheckForInput() 
    {
        IsBackButtonPressed = InputManager.GetButtonDown(Constants.Cancel);
        IsSubmitButtonPressed = InputManager.GetButtonDown(Constants.Submit);
        if (InputManager.GetButtonDown(Constants.Horizontal))
        {
            HorizontalAxis = InputManager.GetAxisRaw(Constants.Horizontal);
        }
        else
        {
            HorizontalAxis = 0;
        }
    }
}
