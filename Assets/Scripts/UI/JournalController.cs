using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public Canvas HomeCanvas, EvidenceCanvas, PartyCanvas, DaCanvas, SkillsCanvas;//ControlsCanvas;
    public GameObject Background, JournalPanel;
    public GameObject Player;
    public GameObject MenuItem;
    public GameObject ImagePlaceholder;
    public GameObject SimpleText;
    public GameObject LongText;
    public GameObject FirstHomeButton;
    public GameObject StrainText, StressText, FocusText;
    public GameObject TypeText, PowerText, FpCostText, SkillDescription;
    public Sprite ButtonSprite;
    private PlayerController PlayerController;
    private Inventory PlayerInventory;
    private Rigidbody2D PlayerBody;
    private List<Canvas> CanvasList;
    private JournalState CurrentState;
    private JournalState PreviousState;
    private GameObject CurrentItem;
    private Case ActiveCase;
    private string HomeSelection;
    private const string DetailTag = "JournalDetail";
    private const string CaseLabelTag = "CaseLabel";
    private const string MenuTag = "MenuItem";

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerBody = Player.GetComponent<Rigidbody2D>();
        PlayerInventory = Player.GetComponent<Inventory>();
        CurrentState = JournalState.Home;
        CurrentItem = EventSystem.current.currentSelectedGameObject;
        ActiveCase = PlayerInventory.ActiveCase;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Journal")) {
            ToggleJournal();
        }

        if(Input.GetButtonDown("Submit") && IsActive()) {
            switch(CurrentState) {
                case JournalState.Home:
                    PreviousState = JournalState.Home;
                    CurrentState = GetStateFromHomeSelection(HomeSelection);
                break;
                case JournalState.Skills:
                case JournalState.DefenseAttorneys:
                case JournalState.Party:
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
                case JournalState.Skills:
                case JournalState.DefenseAttorneys:
                case JournalState.Evidence:
                case JournalState.Party:
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
                //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
                var yPos = JournalUiConstants.ButtonYStart;
                switch(CurrentState) {
                    case JournalState.Evidence:
                        DestroyChildren(EvidenceCanvas.transform, new List<string>{DetailTag});

                        var image = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                        image.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        image.GetComponent<Image>().sprite = CurrentItem.GetComponent<ButtonData>().Image;
                        
                        var description = Instantiate(LongText, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                        description.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.LongTextX,
                        JournalUiConstants.LongTextYStart);
                        description.GetComponent<Text>().text = CurrentItem.GetComponent<ButtonData>().Description;
                    break;
                    case JournalState.Party:
                        DestroyChildren(PartyCanvas.transform, new List<string>{DetailTag});
                        var cData = CurrentItem.GetComponent<ButtonData>();
                        var headshot = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, PartyCanvas.transform);
                        headshot.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        headshot.GetComponent<Image>().sprite = cData.Image;
                        
                        StrainText.GetComponent<Text>().text = cData.Strain;
                        StressText.GetComponent<Text>().text = cData.StressCapacity;
                        FocusText.GetComponent<Text>().text = cData.FocusPoints;
                    break;
                    case JournalState.DefenseAttorneys:
                        DestroyChildren(DaCanvas.transform, new List<string>{DetailTag});
                        var dData = CurrentItem.GetComponent<ButtonData>();
                        var dHeadshot = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, DaCanvas.transform);
                        dHeadshot.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        dHeadshot.GetComponent<Image>().sprite = dData.Image;

                        var dDescription = Instantiate(LongText, Vector3.zero, Quaternion.identity, DaCanvas.transform);
                        dDescription.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.LongTextX,
                        JournalUiConstants.LongTextYStart);
                        dDescription.GetComponent<Text>().text = CurrentItem.GetComponent<ButtonData>().Description;
                    break;
                    case JournalState.Skills:
                        var sData = CurrentItem.GetComponent<ButtonData>();

                        TypeText.GetComponent<Text>().text = Enum.GetName(typeof(PriorityTypes), sData.SkillType);
                        PowerText.GetComponent<Text>().text = GetLatentPowerDefinition(sData.LatentPower);
                        FpCostText.GetComponent<Text>().text = sData.FpCost.ToString();
                        SkillDescription.GetComponent<Text>().text = sData.Description.ToString();
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
                PartyCanvas.gameObject.SetActive(false);
                EvidenceCanvas.gameObject.SetActive(false);
                DaCanvas.gameObject.SetActive(false);
                SkillsCanvas.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(FirstHomeButton);
            break;
            case JournalState.Evidence:
                var caseLabel = GameObject.FindWithTag(CaseLabelTag);
                caseLabel.GetComponent<Text>().text = ActiveCase.Name.ToUpper();
                yPos = JournalUiConstants.ButtonLowYStart;
                DestroyChildren(EvidenceCanvas.transform, new List<string>{MenuTag, DetailTag});
                foreach(var e in PlayerInventory.EvidenceList) {
                    var evInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                    evInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    evInst.GetComponentInChildren<Text>().text = e.Name;
                    evInst.tag = MenuTag;

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
            case JournalState.Party:
                DestroyChildren(PartyCanvas.transform, new List<string>{DetailTag, MenuTag});
                foreach(var p in PlayerInventory.PartyList) {
                    var pInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, PartyCanvas.transform);
                    pInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    pInst.GetComponentInChildren<Text>().text = p.Name;
                    pInst.tag = MenuTag;

                    var pData = pInst.GetComponent<ButtonData>();
                    pData.Id = p.Id;
                    pData.Image = p.Headshot;
                    pData.Description = p.JournalDescription;
                    pData.Strain = Enum.GetName(typeof(StrainType), p.Strain);
                    pData.StressCapacity = p.StressCapacity.ToString();
                    pData.FocusPoints = p.FocusPointCapacity.ToString();
                    pData.Skills = p.CurrentSkills;

                    pInst.name = pInst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(pInst);
                    yPos -= JournalUiConstants.ButtonYSpacing;
                    index++;
                }
            break;
            case JournalState.DefenseAttorneys:
                var cLabel = GameObject.FindWithTag(CaseLabelTag);
                cLabel.GetComponent<Text>().text = ActiveCase.Name.ToUpper();
                yPos = JournalUiConstants.ButtonLowYStart;
                DestroyChildren(DaCanvas.transform, new List<string> {DetailTag, MenuTag});
                foreach(var d in ActiveCase.DefenseAttorneys) {
                    var dInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, DaCanvas.transform);
                    dInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    dInst.GetComponentInChildren<Text>().text = d.Name;
                    dInst.tag = MenuTag;

                    var dData = dInst.GetComponent<ButtonData>();
                    dData.Id = d.Id;
                    dData.Image = d.Headshot;
                    dData.Description = d.JournalDescription;

                    dInst.name = dInst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(dInst);
                    yPos -= JournalUiConstants.ButtonYSpacing;
                    index++;
                }
            break;
            case JournalState.Skills:
                DestroyChildren(SkillsCanvas.transform, new List<string>{DetailTag, MenuTag});
                foreach(var s in PlayerInventory.SkillsList) {
                    var sInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, SkillsCanvas.transform);
                    sInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    sInst.GetComponentInChildren<Text>().text = s.Name;
                    sInst.tag = MenuTag;

                    var sData = sInst.GetComponent<ButtonData>();
                    sData.Id = s.Id;
                    sData.Description = s.Description;
                    sData.SkillType = s.Type;
                    sData.LatentPower = s.LatentPower;
                    sData.FpCost = s.FocusPointCost;

                    sInst.name = sInst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(sInst);
                    yPos -= JournalUiConstants.ButtonYSpacing;
                    index++;
                }
            break;
        }
    }

    //used by inspector
    public void SetHomeSelection(string selection) 
    {
        HomeSelection = selection;
    }

    private JournalState GetStateFromHomeSelection(string selection)
    {
        switch(selection) {
            case "Evidence" :
                return JournalState.Evidence;
            case "Party":
                return JournalState.Party;
            case "DefenseAttorneys":
                return JournalState.DefenseAttorneys;
            case "Skills":
                return JournalState.Skills;
            case "Controls":
                return JournalState.Controls;
            default:
                return JournalState.Home;
        }
    }

    private void ToggleJournal() 
    {
        PlayerBody.velocity = Vector2.zero;
        PlayerController.enabled = !PlayerController.enabled;
        CurrentState = JournalState.Home;
        Background.SetActive(!Background.activeInHierarchy);
        JournalPanel.SetActive(!JournalPanel.activeInHierarchy);
        if(JournalPanel.activeInHierarchy) {
            EvidenceCanvas.gameObject.SetActive(false);
            PartyCanvas.gameObject.SetActive(false);
            HomeCanvas.gameObject.SetActive(true);
            DaCanvas.gameObject.SetActive(false);
            SkillsCanvas.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(FirstHomeButton);
        }
    }

    private bool IsActive() {
        return JournalPanel.activeInHierarchy && Background.activeInHierarchy;
    }

    private void DestroyChildren(Transform transform, List<string> tags = null) 
    {
        var childCount = transform.childCount;
        if(tags == null) {
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
                if(tags.Contains(children[i].tag))
                    DestroyImmediate(children[i]);
            }
        }
    }

    private string GetLatentPowerDefinition(int power)
    {
        if(power > 0 && power < 10) {
            return Constants.Light;
        }
        else if(power > 9 && power < 20) {
            return Constants.Medium;
        }
        else if(power > 19) {
            return Constants.Heavy;
        }
        else {
            return string.Empty;
        }
    }
}
