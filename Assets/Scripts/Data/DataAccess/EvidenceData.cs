using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EvidenceData {
    private readonly IAddressableWrapper addressableWrapper;

    public EvidenceData(IAddressableWrapper addressableWrapper) {
        this.addressableWrapper = addressableWrapper;
    }

    public async Task<Evidence> GetEvidenceById(int id) {
        Evidence targetEvidence = null;
        AsyncOperationHandle<IList<Evidence>> evidenceAssets = addressableWrapper.LoadAssets<Evidence>(AddressablePaths.Evidence,
         e => { if(e.Id == id) targetEvidence = e; });
        addressableWrapper.ReleaseAssets<Evidence>(evidenceAssets);
        await evidenceAssets.Task;
        return targetEvidence;
    }
}