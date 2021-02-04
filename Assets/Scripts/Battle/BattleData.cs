using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour
{
    public Case CaseData;
    public Dictionary<GameObject, Character> TotalParty;
    public Dictionary<GameObject, Character> StartingParty;
    public Dictionary<GameObject, Character> TotalDefenseParty;
    public Dictionary<GameObject, Character> StartingDefenseParty;
    public KeyValuePair<GameObject, Character> Defendant;
    public List<Evidence> EvidenceList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
