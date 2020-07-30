using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public Canvas HomeCanvas, CaseEvidenceCanvas;//, EvidenceCanvas, PartyCanvas, CaseDaCanvas, DaCanvas, ControlsCanvas;
    public GameObject Background, JournalPanel;
    public GameObject Player;
    public GameObject MenuItem;
    public GameObject FirstHomeButton;
    public Sprite ButtonSprite;
    private PlayerController PlayerController;
    private Inventory PlayerInventory;
    private Rigidbody2D PlayerBody;
    private List<Canvas> CanvasList;
    private JournalState CurrentState;
    private JournalState PreviousState;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerBody = Player.GetComponent<Rigidbody2D>();
        PlayerInventory = Player.GetComponent<Inventory>();
        CurrentState = JournalState.Home;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Journal")) {
            PlayerBody.velocity = Vector2.zero;
            PlayerController.enabled = !PlayerController.enabled;
            ToggleJournal();
        }

        if(Input.GetButtonDown("Submit") && IsActive()) {
            switch(CurrentState) {
                case JournalState.Home:
                    CurrentState = JournalState.CaseEvidence;
                    PreviousState = JournalState.Home;
                break;
            }
            UpdateJournalPage(CurrentState);
        }

        if(Input.GetButtonDown("Cancel") && IsActive()) {
            switch(CurrentState) {
                case JournalState.Home:
                    ToggleJournal();
                break;
                case JournalState.CaseEvidence:
                    CurrentState = PreviousState;
                    PreviousState = JournalState.Home;
                break;
            }
            UpdateJournalPage(CurrentState);
        }
    }
    
    private void UpdateJournalPage(JournalState state) 
    {
        switch(state) {
            case JournalState.Home:
                HomeCanvas.gameObject.SetActive(true);
                CaseEvidenceCanvas.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(FirstHomeButton);
            break;
            case JournalState.CaseEvidence:
                DestroyChildren(CaseEvidenceCanvas.transform);
                var yPos = JournalUiConstants.ButtonYStart;
                var index = 0;
                foreach(var c in PlayerInventory.ActiveCases) {
                    var inst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, CaseEvidenceCanvas.transform);

                    inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    inst.GetComponentInChildren<Text>().text = c.Name;

                    inst.name = inst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(inst);

                    index++;
                }
            break;
        }
    }

    private void ToggleJournal() 
    {
        Background.SetActive(!Background.activeInHierarchy);
        JournalPanel.SetActive(!JournalPanel.activeInHierarchy);
        if(JournalPanel.activeInHierarchy) {
            CaseEvidenceCanvas.gameObject.SetActive(false);
            HomeCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(FirstHomeButton);
        }
        else {
            return;
        }
    }

    private bool IsActive() {
        return JournalPanel.activeInHierarchy && Background.activeInHierarchy;
    }

    private void DestroyChildren(Transform transform) 
    {
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
