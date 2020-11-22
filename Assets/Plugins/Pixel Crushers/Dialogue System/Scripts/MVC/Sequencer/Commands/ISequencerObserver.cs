public interface ISequencerObserver {
    void UpdateData(string data);
    void ClearData();
    bool IsUpdated();
}