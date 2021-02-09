using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton : MonoBehaviour
{
    #pragma warning disable 0649
    [NonSerialized]
    public PlayerInventory PlayerInventory;
    [NonSerialized]
    public GuildInventory GuildInventory;
    [NonSerialized]
    public Budget Budget;

    #region Debugfields
    [SerializeField]
    private bool UseTestData;
    [SerializeField]
    private List<Evidence> startEvidenceList;
    [SerializeField]
    private List<Skill> startSkillsList;
    [SerializeField]
    private List<Character> startPartyList;
    [SerializeField]
    private List<Character> guildCharacterList;
    [SerializeField]
    private List<Skill> guildSkillList;
    [SerializeField]
    private int currentBudget;
    [SerializeField]
    private int maxBudget;
    #endregion
    public CaseData caseData;
    public EvidenceData evidenceData;
    public static GameDataSingleton gameData;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (gameData == null)
            gameData = this;
        else
            Destroy(this);
    }

    async void Start()
    {
        if(PlayerInventory == null) {
            if(UseTestData) {
                PlayerInventory = new PlayerInventory();
                startEvidenceList.ForEach(e => PlayerInventory.AddEvidence(e));
                startPartyList.ForEach(p => PlayerInventory.AddPartyMember(p));
                startSkillsList.ForEach(s => PlayerInventory.AddSkill(s));
            }
            else {
                PlayerInventory = new PlayerInventory();
                //Load from save system on initialization
            }
        }
        caseData = new CaseData(new AddressableWrapper(), PlayerInventory);
        await caseData.loadAllCasesFromAddressablesAsync();
        caseData.LoadCasesToInventory();

        evidenceData = new EvidenceData(new AddressableWrapper());
        await evidenceData.loadAllEvidenceFromAddressablesAsync();

        if(GuildInventory == null) {
            if(UseTestData) {
                GuildInventory = new GuildInventory();
                guildCharacterList.ForEach(g => GuildInventory.AddPartyMember(g));
                guildSkillList.ForEach(s => GuildInventory.AddSkill(s));
            }
            else {
                GuildInventory = new GuildInventory();
                //Load from save system on initialization
            }
        }

        if(Budget == null) {
            if(UseTestData) {
                Budget = new Budget();
                Budget.SetCurrentBudget(currentBudget);
                Budget.SetMaxBudget(maxBudget);
            }
            else {
                Budget = new Budget();
                //load budget
            }
        }
        
    }
}
