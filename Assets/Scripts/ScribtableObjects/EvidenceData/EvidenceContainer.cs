using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceContainer", menuName = "Evidence Container")]
public class EvidenceContainer : ScriptableObject
{
    public List<Evidence> AllEvidence;

    public Evidence GetEvidenceById(int id) {
        return AllEvidence != null ? AllEvidence.FirstOrDefault(e => e.Id == id) : null;
    }
}
