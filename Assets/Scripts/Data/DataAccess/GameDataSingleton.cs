using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameDataSingleton
{
    public PlayerInventory PlayerInventory;
    public GuildInventory GuildInventory;
    public Budget Budget;
    public CaseData CaseData;
    public EvidenceData EvidenceData;

    public GameDataSingleton(PlayerInventory playerInventory, GuildInventory guildInventory,
     Budget budget, CaseData caseData, EvidenceData evidenceData) {
        PlayerInventory = playerInventory;
        GuildInventory = guildInventory;
        Budget = budget;
        CaseData = caseData;
        EvidenceData = evidenceData;
    }

    public async Task LoadSaveData(GameDataDebugSettings debugSettings) {
        if(debugSettings.UseTestData) {
            debugSettings.StartEvidenceList.ForEach(e => PlayerInventory.AddEvidence(e));
            debugSettings.StartPartyList.ForEach(p => PlayerInventory.AddPartyMember(p));
            debugSettings.StartSkillsList.ForEach(s => PlayerInventory.AddSkill(s));
        }
        else {
            //Load player inventory from save system on initialization
        }

        if(debugSettings.UseTestData) {
            debugSettings.GuildCharacterList.ForEach(g => GuildInventory.AddPartyMember(g));
            debugSettings.GuildSkillList.ForEach(s => GuildInventory.AddSkill(s));
        }
        else {
            //Load guild inventory from save system on initialization
        }

        if(debugSettings.UseTestData) {
            Budget.SetCurrentBudget(debugSettings.CurrentBudget);
            Budget.SetMaxBudget(debugSettings.MaxBudget);
        }
        else {
            //load budget
        }

        await CaseData.LoadCasesToInventory();
    }
}
