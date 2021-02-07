using System.Linq;
using System.Collections.Generic;

public class PlayerInventory: Inventory
{
    public PlayerInventory(): base()
    {
        evidenceList = new List<Evidence>();
        activeCases = new List<Case>();
    }
    public List<Evidence> EvidenceList { 
        get 
        {
            return evidenceList;
        }
    }
    private List<Evidence> evidenceList;

    public List<Case> ActiveCases
    {
        get 
        {
            return activeCases;
        }
    }
    private List<Case> activeCases;

    public void AddEvidence(Evidence evidence) 
    {
        if(evidenceList.TrueForAll(e => e.Id != evidence.Id))
            evidenceList.Add(evidence);
    }

    public void AddActiveCase(Case c)
    {
        if(activeCases.TrueForAll(ac => ac.Id != c.Id))
            activeCases.Add(c);
    }

    public void ClearCases()
    {
        activeCases.Clear();
    }
}
