using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerObserver: ISequencerObserver
    {
        public List<string> evidenceIds {get; set;}
        public int DataCount {
            get {
                return evidenceIds == null ? 0 : evidenceIds.Count;
            }
        }
        private SequencerObserver() {
            evidenceIds = new List<string>();
        }
        private static SequencerObserver instance;
        public static SequencerObserver Instance {
            get {
                if(instance == null)
                    instance = new SequencerObserver();
                return instance;
            }
        }

        public void UpdateData(string id) 
        {
            evidenceIds.Add(id);
        }

        public void ClearData() {
            evidenceIds.Clear();
        }
    }

}

