using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public Canvas HomeCanvas, CaseEvidenceCanvas, EvidenceCanvas;//, PartyCanvas, CaseDaCanvas, DaCanvas, ControlsCanvas;
    public GameObject Background, JournalPanel;
    public GameObject Player;
    public GameObject MenuItem;
    public GameObject ImagePlaceholder;
    public GameObject SimpleText;
    public GameObject LongText;
    public GameObject FirstHomeButton;
    public Sprite ButtonSprite;
    private PlayerController PlayerController;
    private Inventory PlayerInventory;
    private Rigidbody2D PlayerBody;
    private List<Canvas> CanvasList;
    private JournalState CurrentState;
    private JournalState PreviousState;
    private GameObject CurrentItem;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerBody = Player.GetComponent<Rigidbody2D>();
        PlayerInventory = Player.GetComponent<Inventory>();
        CurrentState = JournalState.Home;
        CurrentItem = EventSystem.current.currentSelectedGameObject;
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
                case JournalState.Home: //WRONG
                    CurrentState = JournalState.CaseEvidence;
                    PreviousState = JournalState.Home;
                break;
                case JournalState.CaseEvidence:
                    CurrentState = JournalState.Evidence;
                    PreviousState = JournalState.CaseEvidence;
                break;
                case JournalState.Evidence:
                    return;
            }
            UpdateJournalPage(CurrentState);
        }
        else if(Input.GetButtonDown("Cancel") && IsActive()) {
            switch(CurrentState) {
                case JournalState.Home:
                    ToggleJournal();
                break;
                case JournalState.CaseEvidence:
                    CurrentState = PreviousState;
                    PreviousState = JournalState.Home;
                break;
                case JournalState.Evidence:
                    CurrentState = PreviousState;
                    PreviousState = JournalState.Home;
                break;
            }
            UpdateJournalPage(CurrentState);
        }
        else {
            //Selecting new menu item
            if(CurrentItem != EventSystem.current.currentSelectedGameObject) {
                CurrentItem = EventSystem.current.currentSelectedGameObject;
                Debug.Log(EventSystem.current.currentSelectedGameObject.name);
                var yPos = JournalUiConstants.ButtonYStart;
                switch(CurrentState) {
                    case JournalState.CaseEvidence:
                        DestroyChildren(CaseEvidenceCanvas.transform, "JournalDetail");
                        foreach(var e in PlayerInventory.EvidenceList) {
                            if(e.ParentCase.Id == CurrentItem.GetComponent<ButtonData>().Id) {
                                var inst = Instantiate(SimpleText, Vector3.zero, Quaternion.identity, CaseEvidenceCanvas.transform);

                                inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXRightPage, yPos);
                                inst.GetComponentInChildren<Text>().text = e.Name;

                                yPos -= JournalUiConstants.ButtonYSpacing;
                            }
                        }
                    break;
                    case JournalState.Evidence:
                        DestroyChildren(EvidenceCanvas.transform, "JournalDetail");

                        var image = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                        image.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        image.GetComponent<Image>().sprite = CurrentItem.GetComponent<ButtonData>().Image;
                        
                        var description = Instantiate(LongText, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                        description.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.LongTextX,
                        JournalUiConstants.LongTextYStart);
                        description.GetComponent<Text>().text = CurrentItem.GetComponent<ButtonData>().Description;
                    break;
                }
            }
        }
    }
    
    private void UpdateJournalPage(JournalState state) 
    {
        var yPos = JournalUiConstants.ButtonYStart;
        var index = 0;
        switch(state) {
            case JournalState.Home:
                HomeCanvas.gameObject.SetActive(true);
                CaseEvidenceCanvas.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(FirstHomeButton);
            break;
            case JournalState.CaseEvidence:
                EvidenceCanvas.gameObject.SetActive(false);
                CaseEvidenceCanvas.gameObject.SetActive(true);
                DestroyChildren(CaseEvidenceCanvas.transform);
                foreach(var c in PlayerInventory.ActiveCases) {
                    var caseInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, CaseEvidenceCanvas.transform);
                    caseInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    caseInst.GetComponentInChildren<Text>().text = c.Name;
                    var caseData = caseInst.GetComponent<ButtonData>();
                    caseData.Id = c.Id;

                    caseInst.name = caseInst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(caseInst);

                    yPos -= JournalUiConstants.ButtonYSpacing;
                    index++;
                }
            break;
            case JournalState.Evidence:
                CaseEvidenceCanvas.gameObject.SetActive(false);
                EvidenceCanvas.gameObject.SetActive(true);
                DestroyChildren(EvidenceCanvas.transform);
                foreach(var e in PlayerInventory.EvidenceList) {
                    var evInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                    evInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    evInst.GetComponentInChildren<Text>().text = e.Name;

                    var evData = evInst.GetComponent<ButtonData>();
                    evData.Id = e.Id;
                    evData.Image = e.Image;
                    evData.Description = e.Description;

                    evInst.name = evInst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(evInst);

                    yPos -= JournalUiConstants.ButtonYSpacing;
                    index++;
                }
            break;
        }
    }

    private void ToggleJournal() 
    {
        CurrentState = JournalState.Home;
        Background.SetActive(!Background.activeInHierarchy);
        JournalPanel.SetActive(!JournalPanel.activeInHierarchy);
        if(JournalPanel.activeInHierarchy) {
            CaseEvidenceCanvas.gameObject.SetActive(false);
            EvidenceCanvas.gameObject.SetActive(false);
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

    private void DestroyChildren(Transform transform, string tag = null) 
    {
        var childCount = transform.childCount;
        if(tag == null) {
            while (childCount > 0) {
                DestroyImmediate(transform.GetChild(0).gameObject);
                childCount--;
            }
        }
        else {
            GameObject[] children = new GameObject[childCount];
            var index = 0;
            foreach(Transform c in transform) {
                children[index] = c.gameObject;
                index++;
            }
            for(var i = 0; i < childCount; i++) {
                if(children[i].tag == tag)
                    DestroyImmediate(children[i]);
            }
        }
    }
}
