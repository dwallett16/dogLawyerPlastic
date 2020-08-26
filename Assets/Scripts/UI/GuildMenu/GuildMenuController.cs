using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildMenuController : MonoBehaviour
{
    public GameObject HireCanvas, BuyCanvas;
    public GameObject HireDescription, HireStrain, HireFp, HireStress, HireList;
    public GameObject BuyDescription, BuyType, BuyPower, BuyCost, BuyList;
    public GameObject MenuItem;
    public GameObject ItemName;
    public GameObject ItemCost;
    public GameObject ConfirmPanel;
    public GameObject ConfirmYes;
    public GameObject MessagePanel;
    public GameObject MessageText;
    public GameObject ConfirmText;
    public GameObject BudgetText;
    public GameObject Background;
    public Sprite HireSprite, BuySprite, FireSprite, SellSprite;
    
    private GuildState currentState;
    private GuildState previousState;
    private GameObject currentItem;
    private GameObject selectedItem;
    private ScrollWithKey hireScrollview;
    private ScrollWithKey buyScrollview;
    private bool isConfirmation;
    private bool isSuccessfulTransaction;
    private GuildState[] tabs = new GuildState[] {GuildState.Hire, GuildState.Buy, GuildState.Fire, GuildState.Sell};
    private Sprite[] tabSprites;
    private int tabIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        hireScrollview = HireCanvas.GetComponent<ScrollWithKey>();
        buyScrollview = BuyCanvas.GetComponent<ScrollWithKey>();
        tabSprites = new Sprite[] {HireSprite, BuySprite, FireSprite, SellSprite};
        var data = GameDataSingleton.gameData;
        BudgetText.GetComponent<Text>().text = data.Budget.CurrentBudget.ToString() + "/" + data.Budget.MaxBudget.ToString();
        currentState = GuildState.Hire;
        UpdateGuildData(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(Constants.Submit)) {

            if(currentState != GuildState.Confirm  && currentState != GuildState.Message) {
                previousState = currentState;
                currentState = GuildState.Confirm;
            }
            else if(currentState == GuildState.Confirm) {
               HandleConfirmation();
            }
            else if(currentState == GuildState.Message) {
                currentState = previousState;
            }

            UpdateGuildData(currentState);
        }

        if(currentItem != EventSystem.current.currentSelectedGameObject) {
            currentItem = EventSystem.current.currentSelectedGameObject;
            Debug.Log(currentItem.name);
            switch(currentState) {
                case GuildState.Fire:
                case GuildState.Hire:
                    var cData = currentItem.GetComponent<ButtonData>();
                    HireDescription.GetComponent<Text>().text = cData.Description;
                    HireStrain.GetComponent<Text>().text = cData.Strain;
                    HireFp.GetComponent<Text>().text = cData.FocusPoints;
                    HireStress.GetComponent<Text>().text = cData.StressCapacity;
                break;
                case GuildState.Sell:
                case GuildState.Buy:
                    var sData = currentItem.GetComponent<ButtonData>();
                    BuyDescription.GetComponent<Text>().text = sData.Description;
                    BuyType.GetComponent<Text>().text = sData.SkillType.ToString();
                    BuyPower.GetComponent<Text>().text = Constants.GetLatentPowerDefinition(sData.LatentPower);
                    BuyCost.GetComponent<Text>().text = sData.FpCost.ToString();
                break;
                case GuildState.Message:
                case GuildState.Confirm:
                break;
            }
        }

        if(Input.GetButtonDown(Constants.Horizontal)) {
            var dir = Input.GetAxisRaw(Constants.Horizontal) < 0 ? -1 : 1;
            if(tabIndex + dir >= tabs.Length)
                tabIndex = tabs.Length - 1;
            else if(tabIndex + dir < 0)
                tabIndex = 0;
            else
                tabIndex += dir;
            currentState = tabs[tabIndex];
            Background.GetComponent<Image>().sprite = tabSprites[tabIndex];

            UpdateGuildData(currentState);
        }
    }

    void UpdateGuildData(GuildState state) {
        ConfirmPanel.SetActive(false);
        MessagePanel.SetActive(false);
        hireScrollview.ScrollToTop();
        buyScrollview.ScrollToTop();
        var yPos = GuildUiConstants.MenuY;
        var index = 0;
        switch(state) {
            case GuildState.Hire:
                HireCanvas.SetActive(true);
                BuyCanvas.SetActive(false);
                DestroyChildren(HireList.transform, new List<string>{Constants.MenuTag});
                foreach(var p in GameDataSingleton.gameData.GuildInventory.PartyList) {
                    var pInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, HireList.transform);
                    pInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(GuildUiConstants.MenuX, yPos);
                    pInst.name = pInst.name + index;
                    var pName = Instantiate(ItemName, Vector3.zero, Quaternion.identity, pInst.transform);
                    pName.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    pName.GetComponent<Text>().text = p.Name;
                    var pCost = Instantiate(ItemCost, Vector3.zero, Quaternion.identity, pInst.transform);
                    pCost.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    pCost.GetComponent<Text>().text = p.Price.ToString() + "f";
                    
                    var data = pInst.GetComponent<ButtonData>();
                    data.Id = p.Id;
                    data.Description = p.JournalDescription;
                    data.Strain = p.Strain.ToString();
                    data.FocusPoints = p.FocusPointCapacity.ToString();
                    data.StressCapacity = p.StressCapacity.ToString();
                    data.Price = p.Price;

                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(pInst);
                    yPos -= GuildUiConstants.MenuYSpacing;
                    index++;
                }
            break;
            case GuildState.Buy:
                HireCanvas.SetActive(false);
                BuyCanvas.SetActive(true);
                DestroyChildren(BuyList.transform, new List<string>{Constants.MenuTag});
                foreach(var s in GameDataSingleton.gameData.GuildInventory.SkillsList) {
                    var sInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, BuyList.transform);
                    sInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(GuildUiConstants.MenuX, yPos);
                    sInst.name = sInst.name + index;
                    var sName = Instantiate(ItemName, Vector3.zero, Quaternion.identity, sInst.transform);
                    sName.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    sName.GetComponent<Text>().text = s.Name;
                    var pCost = Instantiate(ItemCost, Vector3.zero, Quaternion.identity, sInst.transform);
                    pCost.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    pCost.GetComponent<Text>().text = s.Price.ToString() + "f";
                    
                    var data = sInst.GetComponent<ButtonData>();
                    data.Id = s.Id;
                    data.Description = s.Description;
                    data.SkillType = s.Type;
                    data.LatentPower = s.LatentPower;
                    data.FpCost = s.FocusPointCost;
                    data.Price = s.Price;

                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(sInst);
                    yPos -= GuildUiConstants.MenuYSpacing;
                    index++;
                }
            break;
            case GuildState.Fire:
                HireCanvas.SetActive(true);
                BuyCanvas.SetActive(false);
                DestroyChildren(HireList.transform, new List<string>{Constants.MenuTag});
                foreach(var p in GameDataSingleton.gameData.PlayerInventory.PartyList) {
                    var pInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, HireList.transform);
                    pInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(GuildUiConstants.MenuX, yPos);
                    pInst.name = pInst.name + index;
                    var pName = Instantiate(ItemName, Vector3.zero, Quaternion.identity, pInst.transform);
                    pName.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    pName.GetComponent<Text>().text = p.Name;
                    var pCost = Instantiate(ItemCost, Vector3.zero, Quaternion.identity, pInst.transform);
                    pCost.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    pCost.GetComponent<Text>().text = p.Price.ToString() + "f";
                    
                    var data = pInst.GetComponent<ButtonData>();
                    data.Id = p.Id;
                    data.Description = p.JournalDescription;
                    data.Strain = p.Strain.ToString();
                    data.FocusPoints = p.FocusPointCapacity.ToString();
                    data.StressCapacity = p.StressCapacity.ToString();
                    data.Price = p.Price;

                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(pInst);
                    yPos -= GuildUiConstants.MenuYSpacing;
                    index++;
                }
            break;
            case GuildState.Sell:
                HireCanvas.SetActive(false);
                BuyCanvas.SetActive(true);
                DestroyChildren(BuyList.transform, new List<string>{Constants.MenuTag});
                foreach(var s in GameDataSingleton.gameData.PlayerInventory.SkillsList) {
                    var sInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, BuyList.transform);
                    sInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(GuildUiConstants.MenuX, yPos);
                    sInst.name = sInst.name + index;
                    var sName = Instantiate(ItemName, Vector3.zero, Quaternion.identity, sInst.transform);
                    sName.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    sName.GetComponent<Text>().text = s.Name;
                    var pCost = Instantiate(ItemCost, Vector3.zero, Quaternion.identity, sInst.transform);
                    pCost.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    pCost.GetComponent<Text>().text = s.Price.ToString() + "f";
                    
                    var data = sInst.GetComponent<ButtonData>();
                    data.Id = s.Id;
                    data.Description = s.Description;
                    data.SkillType = s.Type;
                    data.LatentPower = s.LatentPower;
                    data.FpCost = s.FocusPointCost;
                    data.Price = s.Price;

                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(sInst);
                    yPos -= GuildUiConstants.MenuYSpacing;
                    index++;
                }
            break;
            case GuildState.Confirm:
                selectedItem = currentItem;
                ConfirmPanel.SetActive(true);
                ConfirmText.GetComponent<Text>().text = GetConfirmText(previousState);
                EventSystem.current.SetSelectedGameObject(ConfirmYes);
            break;
            case GuildState.Message:
                MessagePanel.SetActive(true);
                MessageText.GetComponent<Text>().text = GetMessageText(previousState);
            break;
        }
    }

    public void ConfirmSelection() 
    {
        isConfirmation = true;
    }

    public void CancelSelection()
    {
        isConfirmation = false;
    }

    private void HandleConfirmation() 
    {
        if(isConfirmation) {
            var id = selectedItem.GetComponent<ButtonData>().Id;
            var price = selectedItem.GetComponent<ButtonData>().Price;
            var gameData = GameDataSingleton.gameData;
            isSuccessfulTransaction = false;
            switch(previousState) {
                case GuildState.Hire:
                    if(gameData.Budget.CurrentBudget >= price) {
                        gameData.PlayerInventory.AddPartyMember(gameData.GuildInventory.GetCharacterById(id));
                        gameData.Budget.Purchase(price);
                        isSuccessfulTransaction = true;
                    }
                break;
                case GuildState.Buy:
                    if(gameData.Budget.CurrentBudget >= price) {
                        gameData.PlayerInventory.AddSkill(gameData.GuildInventory.GetSkillById(id));
                        gameData.Budget.Purchase(price);
                        isSuccessfulTransaction = true;
                    }
                    
                break;
                case GuildState.Fire:
                    gameData.PlayerInventory.RemovePartyMemberById(id);
                    gameData.Budget.Sell(price);
                    isSuccessfulTransaction = true;
                break;
                case GuildState.Sell:
                    gameData.PlayerInventory.RemoveSkillById(id);
                    gameData.Budget.Sell(price);
                    isSuccessfulTransaction = true;
                break;
            }
            currentState = GuildState.Message;
            BudgetText.GetComponent<Text>().text = gameData.Budget.CurrentBudget.ToString() + "/" + gameData.Budget.MaxBudget.ToString();
        }
        else {
            currentState = previousState;
        }
    }

    private string GetMessageText(GuildState previousState) 
    {
        var name = selectedItem.GetComponentInChildren<Text>().text;
        if(isSuccessfulTransaction) {
            switch(previousState) {
                case GuildState.Hire:
                    return "Recruited " + name + ".";
                case GuildState.Buy:
                    return "Purchased " + name + ".";
                case GuildState.Fire:
                    return "Fired " + name + ".";
                case GuildState.Sell:
                    return "Sold " + name + ".";
            }
        }
        else {
            return "Insufficient funds.";
        }
        
        return string.Empty;
    }

    private string GetConfirmText(GuildState previousState) 
    {
        var name = selectedItem.GetComponentInChildren<Text>().text;
        switch(previousState) {
            case GuildState.Hire:
                return "Hire prosecutor " + name + "?";
            case GuildState.Buy:
                return "Purchase skill stone " + name + "?";
            case GuildState.Fire:
                return "Fire prosecutor " + name + "?";
            case GuildState.Sell:
                return "Sell skill stone " + name + "?";
        }
        return string.Empty;
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
}
