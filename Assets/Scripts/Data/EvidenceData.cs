using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Linq;

public class EvidenceData {
    private List<Evidence> allEvidence;

    public EvidenceData() {
        allEvidence = new List<Evidence>();
        loadAllEvidence();
    }

    public Evidence GetEvidenceById(int id) {
        return allEvidence != null ? allEvidence.First(e => e.Id == id) : null;
    }

    private void loadAllEvidence() {
        var evidenceAssets = Addressables.LoadAssetsAsync<Evidence>(AddressablePaths.Evidence, e => {
                allEvidence.Add(e);
            });
        evidenceAssets.Completed += a => Addressables.Release(evidenceAssets);
    }

}