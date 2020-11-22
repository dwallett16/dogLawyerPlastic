using System;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.SequencerCommands;

public class GameDataSingleton : MonoBehaviour
{
    #pragma warning disable 0649
    [NonSerialized]
    public PlayerInventory PlayerInventory;
    [NonSerialized]
    public GuildInventory GuildInventory;
    [NonSerialized]
    public Budget Budget;

    [SerializeField]
    private EvidenceContainer allEvidence;
    [SerializeField]
    private bool UseTestData;
    [SerializeField]
    private List<Evidence> startEvidenceList;
    [SerializeField]
    private List<Skill> startSkillsList;
    [SerializeField]
    private List<Character> startPartyList;
    [SerializeField]
    private List<Case> startCaseList;
    [SerializeField]
    private List<Character> guildCharacterList;
    [SerializeField]
    private List<Skill> guildSkillList;
    [SerializeField]
    private int currentBudget;
    [SerializeField]
    private int maxBudget;
    
    public static GameDataSingleton gameData;

    private SequencerObserverContainer observerContainer;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (gameData == null)
            gameData = this;
        else
            Destroy(this);

    }

    void Start()
    {
        observerContainer = SequencerObserverContainer.Instance;
        if(PlayerInventory == null) {
            if(UseTestData) {
                PlayerInventory = new PlayerInventory();
                startEvidenceList.ForEach(e => PlayerInventory.AddEvidence(e));
                startPartyList.ForEach(p => PlayerInventory.AddPartyMember(p));
                startSkillsList.ForEach(s => PlayerInventory.AddSkill(s));
                startCaseList.ForEach(c => PlayerInventory.AddActiveCase(c));
            }
            else {
                PlayerInventory = new PlayerInventory();
                //Load from save system on initialization
            }
        }
        if(GuildInventory == null) {
            if(UseTestData) {
                GuildInventory = new GuildInventory();
                guildCharacterList.ForEach(g => GuildInventory.AddPartyMember(g));
                guildSkillList.ForEach(s => GuildInventory.AddSkill(s));
            }
            else {
                GuildInventory = new GuildInventory();
                //Load here
            }
        }
        if(Budget == null) {
            if(UseTestData) {
                Budget.SetCurrentBudget(currentBudget);
                Budget.SetMaxBudget(maxBudget);
            }
            else {
                Budget = new Budget();
                //load budget
            }
        }
        
    }

    void Update() 
    {   
        if(observerContainer.AddEvidenceObserver != null && observerContainer.AddEvidenceObserver.IsUpdated()) {
            var evObserver = observerContainer.AddEvidenceObserver as AddEvidenceObserver;
            foreach(var id in evObserver.evidenceIds) {
                var outId = Int32.Parse(id);
                PlayerInventory.AddEvidence(allEvidence.GetEvidenceById(outId));
            }
            evObserver.ClearData();
        }
    }

}
