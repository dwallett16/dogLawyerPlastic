using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EvidenceData {
    private readonly IAddressableWrapper addressableWrapper;
    private List<Evidence> allEvidence;

    public EvidenceData(IAddressableWrapper addressableWrapper) {
        this.addressableWrapper = addressableWrapper;
        allEvidence = new List<Evidence>();
    }

    //Testing only
    public EvidenceData(IAddressableWrapper addressableWrapper, List<Evidence> allEvidence) {
        this.addressableWrapper = addressableWrapper;
        this.allEvidence = allEvidence;
    }

    public Evidence GetEvidenceById(int id) {
        return allEvidence.First(e => e.Id == id);
    }

    public async Task<IList<Evidence>> loadAllEvidenceFromAddressablesAsync() {
        AsyncOperationHandle<IList<Evidence>> evidenceAssets = addressableWrapper.LoadAssets<Evidence>(AddressablePaths.Evidence, e => {
                allEvidence.Add(e);
            });
        addressableWrapper.ReleaseAssets<Evidence>(evidenceAssets);
        return await evidenceAssets.Task;
    }

}