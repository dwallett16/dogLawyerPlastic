using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

public class SequencerObserverContainer {
    public List<ISequencerObserver> Observers;

    public void AttachObserver(ISequencerObserver observer) {
        Observers.Add(observer);
    }
}

}