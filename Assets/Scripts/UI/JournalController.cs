using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    public Canvas HomeCanvas, CaseEvidenceCanvas;//, EvidenceCanvas, PartyCanvas, CaseDaCanvas, DaCanvas, ControlsCanvas;
    public GameObject Background, JournalPanel;
    public GameObject Player;
    private PlayerController PlayerController;
    private Rigidbody2D PlayerBody;
    private List<Canvas> CanvasList;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerBody = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Journal")) 
        {
            ToggleJournal();
        }
    }

    private void ToggleJournal() 
    {
        Background.SetActive(!Background.activeInHierarchy);
        JournalPanel.SetActive(!JournalPanel.activeInHierarchy);
        CaseEvidenceCanvas.enabled = !CaseEvidenceCanvas.enabled;
        PlayerBody.velocity = Vector2.zero;
        PlayerController.enabled = !PlayerController.enabled;
    }
}
