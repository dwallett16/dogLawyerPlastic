using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class AddEvidenceObserver: ISequencerObserver
    {
        public List<string> evidenceIds {get; set;}
        public int DataCount {
            get {
                return evidenceIds == null ? 0 : evidenceIds.Count;
            }
        }
        public AddEvidenceObserver() {
            evidenceIds = new List<string>();
        }

        public void UpdateData(string id) 
        {
            evidenceIds.Add(id);
        }

        public void ClearData() {
            evidenceIds.Clear();
        }

        public bool IsUpdated() {
            return DataCount > 0;
        }
    }

}

