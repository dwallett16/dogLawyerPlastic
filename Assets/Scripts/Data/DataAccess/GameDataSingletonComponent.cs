using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingletonComponent : MonoBehaviour
{
    public static GameDataSingleton gameData;

    
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

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (gameData == null)
            gameData = CreateGameDataInstance();
        else
            Destroy(this);
    }

    async void Start()
    {
        var debugSettings = new GameDataDebugSettings {
            UseTestData = this.UseTestData,
            CurrentBudget = currentBudget,
            GuildCharacterList = guildCharacterList,
            GuildSkillList = guildSkillList,
            MaxBudget = maxBudget,
            StartEvidenceList = startEvidenceList,
            StartPartyList = startPartyList,
            StartSkillsList = startSkillsList
        };
        await gameData.LoadSaveData(debugSettings);
    }

    private GameDataSingleton CreateGameDataInstance() {
        var playerInventory = new PlayerInventory();
        var guildInventory = new GuildInventory();
        var budget = new Budget();
        var addressableWrapper = new AddressableWrapper();
        var caseData = new CaseData(addressableWrapper, playerInventory);
        var evidenceData = new EvidenceData(addressableWrapper);
        return new GameDataSingleton(playerInventory, guildInventory, budget, caseData, evidenceData);
    }
}
