using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDebugMenu : MonoBehaviour
{
    public bool UseTestData;
    public Case CaseData;
    public List<Character> TotalParty;
    public List<Character> StartingParty;
    public List<Evidence> EvidenceList;


    // Start is called before the first frame update
    void Start()
    {
        if(StartingParty.Count > 2 || EvidenceList.Count > 3) {
            throw new System.Exception("Incorrect debug list sizes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
