using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton : MonoBehaviour
{
    [NonSerialized]
    public Inventory PlayerInventory;

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
    
    private static GameObject gameData;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (gameData == null)
            gameData = gameObject;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if(PlayerInventory == null) {
            PlayerInventory = new Inventory();
            //Load from save system on initialization
            if(UseTestData) {
                PlayerInventory = new Inventory();
                startEvidenceList.ForEach(e => PlayerInventory.AddEvidence(e));
                startPartyList.ForEach(p => PlayerInventory.AddPartyMember(p));
                startSkillsList.ForEach(s => PlayerInventory.AddSkill(s));
                startCaseList.ForEach(c => PlayerInventory.AddActiveCase(c));
            }
        }
    }

}
