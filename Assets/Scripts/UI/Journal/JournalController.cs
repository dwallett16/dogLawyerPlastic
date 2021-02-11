using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public Canvas HomeCanvas, CaseEvidenceCanvas, EvidenceCanvas, PartyCanvas, CaseDaCanvas, DaCanvas, SkillsCanvas;
    public GameObject Background, JournalPanel;
    public GameObject MenuItem;
    public GameObject ImagePlaceholder;
    public GameObject SimpleText;
    public GameObject LongText;
    public GameObject FirstHomeButton;
    public GameObject StrainText, StressText, FocusText;
    public GameObject TypeText, PowerText, FpCostText, SkillDescription;
    public Sprite ButtonSprite;
    public AudioClip OpenClip;
    public AudioClip CloseClip;
    public AudioClip PageTurnClip;
    private GameObject player;
    private IComponentDisabler playerControllerDisabler;
    private Rigidbody2D playerBody;
    private List<Canvas> canvasList;
    private JournalState currentState;
    private JournalState previousState;
    private GameObject currentItem;
    private string homeSelection;
    private Case activeCase;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Constants.PlayerTag);
        playerControllerDisabler = player.GetComponent<IComponentDisabler>();
        playerBody = player.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        currentState = JournalState.Home;
        currentItem = EventSystem.current.currentSelectedGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(Constants.Journal)) {
            ToggleJournal();
        }

        if(Input.GetButtonDown(Constants.Submit) && IsActive()) {
            switch(currentState) {
                case JournalState.Home:
                    previousState = JournalState.Home;
                    currentState = GetStateFromHomeSelection(homeSelection);
                break;
                case JournalState.CaseEvidence:
                    currentState = JournalState.Evidence;
                    previousState = JournalState.CaseEvidence;
                    activeCase = GameDataSingletonComponent.gameData.CaseData.GetCaseById(currentItem.GetComponent<ButtonData>().Id);
                break;
                case JournalState.CaseDefenseAttorneys:
                    currentState = JournalState.DefenseAttorneys;
                    previousState = JournalState.CaseDefenseAttorneys;
                break;
                case JournalState.Skills:
                case JournalState.DefenseAttorneys:
                case JournalState.Party:
                case JournalState.Evidence:
                    return;
            }
            UpdateJournalPage(currentState);
        }
        else if(Input.GetButtonDown(Constants.Cancel) && IsActive()) {
            if(currentState == JournalState.Home) {
                ToggleJournal();
                return;
            }
            currentState = previousState;
            previousState = JournalState.Home;

            UpdateJournalPage(currentState);
        }
        else {
            //Selecting new menu item
            if(currentItem != EventSystem.current.currentSelectedGameObject) {
                currentItem = EventSystem.current.currentSelectedGameObject;
                
                var yPos = JournalUiConstants.ButtonYStart;
                switch(currentState) {
                    case JournalState.CaseEvidence:
                        DestroyChildren(CaseEvidenceCanvas.transform, new List<string>{Constants.DetailTag});
                        foreach(var e in GameDataSingletonComponent.gameData.PlayerInventory.EvidenceList) {
                            if(e.ParentCase.Id == currentItem.GetComponent<ButtonData>().Id) {
                                var inst = Instantiate(SimpleText, Vector3.zero, Quaternion.identity, CaseEvidenceCanvas.transform);

                                inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXRightPage, yPos);
                                inst.GetComponentInChildren<Text>().text = e.Name;

                                yPos -= JournalUiConstants.ButtonYSpacing;
                            }
                        }
                    break;
                    case JournalState.Evidence:
                        DestroyChildren(EvidenceCanvas.transform, new List<string>{Constants.DetailTag});

                        var image = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                        image.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        image.GetComponent<Image>().sprite = currentItem.GetComponent<ButtonData>().Image;
                        
                        var description = Instantiate(LongText, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                        description.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.LongTextX,
                        JournalUiConstants.LongTextYStart);
                        description.GetComponent<Text>().text = currentItem.GetComponent<ButtonData>().Description;
                    break;
                    case JournalState.Party:
                        DestroyChildren(PartyCanvas.transform, new List<string>{Constants.DetailTag});
                        var cData = currentItem.GetComponent<ButtonData>();
                        var headshot = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, PartyCanvas.transform);
                        headshot.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        headshot.GetComponent<Image>().sprite = cData.Image;
                        
                        StrainText.GetComponent<Text>().text = cData.Strain;
                        StressText.GetComponent<Text>().text = cData.StressCapacity;
                        FocusText.GetComponent<Text>().text = cData.FocusPoints;
                    break;
                    case JournalState.CaseDefenseAttorneys:
                        activeCase = GameDataSingletonComponent.gameData.CaseData.GetCaseById(currentItem.GetComponent<ButtonData>().Id);
                        DestroyChildren(CaseDaCanvas.transform, new List<string>{Constants.DetailTag});
                        foreach(var d in activeCase.DefenseAttorneys) {
                            
                            var inst = Instantiate(SimpleText, Vector3.zero, Quaternion.identity, CaseDaCanvas.transform);

                            inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXRightPage, yPos);
                            inst.GetComponentInChildren<Text>().text = d.Name;
                            yPos -= JournalUiConstants.ButtonYSpacing;
                        }
                    break;
                    case JournalState.DefenseAttorneys:
                        DestroyChildren(DaCanvas.transform, new List<string>{Constants.DetailTag});
                        var dData = currentItem.GetComponent<ButtonData>();
                        var dHeadshot = Instantiate(ImagePlaceholder, Vector3.zero, Quaternion.identity, DaCanvas.transform);
                        dHeadshot.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ImageXRightPage,
                        JournalUiConstants.ImageYStart);
                        dHeadshot.GetComponent<Image>().sprite = dData.Image;

                        var dDescription = Instantiate(LongText, Vector3.zero, Quaternion.identity, DaCanvas.transform);
                        dDescription.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.LongTextX,
                        JournalUiConstants.LongTextYStart);
                        dDescription.GetComponent<Text>().text = currentItem.GetComponent<ButtonData>().Description;
                    break;
                    case JournalState.Skills:
                        var sData = currentItem.GetComponent<ButtonData>();

                        TypeText.GetComponent<Text>().text = Enum.GetName(typeof(PriorityTypes), sData.SkillType);
                        PowerText.GetComponent<Text>().text = Constants.GetLatentPowerDefinition(sData.LatentPower);
                        FpCostText.GetComponent<Text>().text = sData.FpCost.ToString();
                        SkillDescription.GetComponent<Text>().text = sData.Description.ToString();
                    break;
                }
            }
        }
    }
    
    private void UpdateJournalPage(JournalState state) 
    {
        PlayAudio(PageTurnClip);
        var yPos = JournalUiConstants.ButtonYStart;
        var index = 0;
        switch(state) {
            case JournalState.Home:
                ActivateHomePage();
                EventSystem.current.SetSelectedGameObject(FirstHomeButton);
            break;
            case JournalState.CaseEvidence:
                EvidenceCanvas.gameObject.SetActive(false);
                CaseEvidenceCanvas.gameObject.SetActive(true);
                DestroyChildren(CaseEvidenceCanvas.transform);
                foreach(var c in GameDataSingletonComponent.gameData.PlayerInventory.ActiveCases) {
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
                EvidenceCanvas.gameObject.SetActive(true);
                CaseEvidenceCanvas.gameObject.SetActive(false);
                var caseLabel = GameObject.FindWithTag(Constants.CaseLabelTag);
                caseLabel.GetComponent<Text>().text = activeCase.Name.ToUpper();
                yPos = JournalUiConstants.ButtonLowYStart;
                DestroyChildren(EvidenceCanvas.transform, new List<string>{Constants.MenuTag, Constants.DetailTag});
                foreach(var e in GameDataSingletonComponent.gameData.PlayerInventory.EvidenceList) {
                    var evInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, EvidenceCanvas.transform);
                    evInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    evInst.GetComponentInChildren<Text>().text = e.Name;
                    evInst.tag = Constants.MenuTag;

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
                DestroyChildren(PartyCanvas.transform, new List<string>{Constants.DetailTag, Constants.MenuTag});
                foreach(var p in GameDataSingletonComponent.gameData.PlayerInventory.PartyList) {
                    var pInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, PartyCanvas.transform);
                    pInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    pInst.GetComponentInChildren<Text>().text = p.Name;
                    pInst.tag = Constants.MenuTag;

                    var pData = pInst.GetComponent<ButtonData>();
                    pData.Id = p.Id;
                    pData.Image = p.Headshot;
                    pData.Description = p.JournalDescription;
                    pData.StressCapacity = p.StressCapacity.ToString();
                    pData.FocusPoints = p.FocusPointCapacity.ToString();
                    pData.Skills = p.Skills;

                    pInst.name = pInst.name + index;
                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(pInst);
                    yPos -= JournalUiConstants.ButtonYSpacing;
                    index++;
                }
            break;
            case JournalState.CaseDefenseAttorneys:
                CaseDaCanvas.gameObject.SetActive(true);
                DaCanvas.gameObject.SetActive(false);
                DestroyChildren(CaseDaCanvas.transform);
                foreach(var c in GameDataSingletonComponent.gameData.PlayerInventory.ActiveCases) {
                    var caseInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, CaseDaCanvas.transform);
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
            case JournalState.DefenseAttorneys:
                CaseDaCanvas.gameObject.SetActive(false);
                DaCanvas.gameObject.SetActive(true);
                var cLabel = GameObject.FindWithTag(Constants.CaseLabelTag);
                cLabel.GetComponent<Text>().text = activeCase.Name.ToUpper();
                yPos = JournalUiConstants.ButtonLowYStart;
                DestroyChildren(DaCanvas.transform, new List<string> {Constants.DetailTag, Constants.MenuTag});
                foreach(var d in activeCase.DefenseAttorneys) {
                    var dInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, DaCanvas.transform);
                    dInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    dInst.GetComponentInChildren<Text>().text = d.Name;
                    dInst.tag = Constants.MenuTag;

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
                DestroyChildren(SkillsCanvas.transform, new List<string>{Constants.DetailTag, Constants.MenuTag});
                foreach(var s in GameDataSingletonComponent.gameData.PlayerInventory.SkillsList) {
                    var sInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, SkillsCanvas.transform);
                    sInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(JournalUiConstants.ButtonXLeftPage, yPos);
                    sInst.GetComponentInChildren<Text>().text = s.Name;
                    sInst.tag = Constants.MenuTag;

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
        homeSelection = selection;
    }

    private JournalState GetStateFromHomeSelection(string selection)
    {
        switch(selection) {
            case "Evidence" :
                return JournalState.CaseEvidence;
            case "Party":
                return JournalState.Party;
            case "DefenseAttorneys":
                return JournalState.CaseDefenseAttorneys;
            case "Skills":
                return JournalState.Skills;
            default:
                return JournalState.Home;
        }
    }

    private void ToggleJournal() 
    {
        playerBody.velocity = Vector2.zero;
        playerControllerDisabler.ToggleComponent();
        currentState = JournalState.Home;
        Background.SetActive(!Background.activeInHierarchy);
        JournalPanel.SetActive(!JournalPanel.activeInHierarchy);
        if(JournalPanel.activeInHierarchy) {
            Time.timeScale = 0;
            ActivateHomePage();
            EventSystem.current.SetSelectedGameObject(FirstHomeButton);
            PlayAudio(OpenClip);
        }
        else {
            Time.timeScale = 1;
            PlayAudio(CloseClip);
        }
    }

    private void ActivateHomePage()
    {
            EvidenceCanvas.gameObject.SetActive(false);
            CaseEvidenceCanvas.gameObject.SetActive(false);
            CaseDaCanvas.gameObject.SetActive(false);
            PartyCanvas.gameObject.SetActive(false);
            HomeCanvas.gameObject.SetActive(true);
            DaCanvas.gameObject.SetActive(false);
            SkillsCanvas.gameObject.SetActive(false);
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

    private void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
