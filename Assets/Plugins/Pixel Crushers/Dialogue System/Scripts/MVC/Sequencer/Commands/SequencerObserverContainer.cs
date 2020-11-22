using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

public class SequencerObserverContainer {
    private SequencerObserverContainer() {}
    private static SequencerObserverContainer instance {get; set;}
    public static SequencerObserverContainer Instance {
        get {
            if(instance == null) {
                instance = new SequencerObserverContainer();
            }
            return instance;
        }
    }

    public ISequencerObserver AddEvidenceObserver {get; set;}

}

}