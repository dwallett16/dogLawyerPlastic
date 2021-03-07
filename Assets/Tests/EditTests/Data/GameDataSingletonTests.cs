using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Data 
{
    public class GameDataSingletonTests
{
    [Test]
    public async void LoadSaveDataUsingDebugTestDataSetsPlayerInventory() 
    {
        var gameData = GetGameData();
        var debugSettings = GetDebugSettings();

        await gameData.LoadSaveData(debugSettings);

        Assert.AreEqual(debugSettings.StartEvidenceList, gameData.PlayerInventory.EvidenceList);
        Assert.AreEqual(debugSettings.StartPartyList, gameData.PlayerInventory.PartyList);
        Assert.AreEqual(debugSettings.StartSkillsList, gameData.PlayerInventory.SkillsList);
    }

    [Test]
    public async void LoadSaveDataUsingDebugTestDataSetsGuildInventory() 
    {
        var gameData = GetGameData();
        var debugSettings = GetDebugSettings();

        await gameData.LoadSaveData(debugSettings);

        Assert.AreEqual(debugSettings.GuildSkillList, gameData.GuildInventory.SkillsList);
        Assert.AreEqual(debugSettings.GuildCharacterList, gameData.GuildInventory.PartyList);
    }

    [Test]
    public async void LoadSaveDataUsingDebugTestDataSetsBudget() 
    {
        var gameData = GetGameData();
        var debugSettings = GetDebugSettings();

        await gameData.LoadSaveData(debugSettings);

        Assert.AreEqual(debugSettings.CurrentBudget, gameData.Budget.CurrentBudget);
        Assert.AreEqual(debugSettings.MaxBudget, gameData.Budget.MaxBudget);
    }

    [Test]
    public async void LoadSaveDataLoadsCasesFromAddressables() 
    {
        var addressableMock = Substitute.For<IAddressableWrapper>();
        var asyncHandleResult = new AsyncOperationHandle<IList<Case>>();
        addressableMock.LoadAssets<Case>(Arg.Any<string>(), Arg.Any<Action<Case>>()).Returns(asyncHandleResult);
        var playerInventory = new PlayerInventory();
        var guildInventory = new GuildInventory();
        var budget = new Budget();
        var caseData = new CaseData(addressableMock, playerInventory);
        var evidenceData = new EvidenceData(addressableMock);
        var gameData = new GameDataSingleton(playerInventory, guildInventory, budget, caseData, evidenceData);
        var debugSettings = GetDebugSettings();

        await gameData.LoadSaveData(debugSettings);

        addressableMock.Received(1).LoadAssets<Case>(AddressablePaths.Cases, Arg.Any<Action<Case>>());
    }

    private GameDataSingleton GetGameData() {
        var playerInventory = new PlayerInventory();
        var guildInventory = new GuildInventory();
        var budget = new Budget();
        var addressableWrapper = Substitute.For<IAddressableWrapper>();
        var caseData = new CaseData(addressableWrapper, playerInventory);
        var evidenceData = new EvidenceData(addressableWrapper);
        return new GameDataSingleton(playerInventory, guildInventory, budget, caseData, evidenceData);
    }

    private GameDataDebugSettings GetDebugSettings() {
        return new GameDataDebugSettings {
            UseTestData = true,
            StartEvidenceList = new List<Evidence> {TestDataFactory.CreateEvidence(0, TestDataFactory.CreateCase(0))},
            StartPartyList = new List<Character> {TestDataFactory.CreateCharacter(0, CharacterType.PlayerCharacter),
            TestDataFactory.CreateCharacter(1, CharacterType.PlayerCharacter)},
            StartSkillsList = new List<Skill> {TestDataFactory.CreateSkill(0), TestDataFactory.CreateSkill(1)},
            GuildCharacterList = new List<Character> { TestDataFactory.CreateCharacter(10, CharacterType.PlayerCharacter) },
            GuildSkillList = new List<Skill> { TestDataFactory.CreateSkill(10) },
            CurrentBudget = 1000,
            MaxBudget = 5000
        };
    }
}
}
