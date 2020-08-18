using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildMenuController : MonoBehaviour
{
    public GameObject HireCanvas, BuyCanvas, FireCanvas, SellCanvas;
    public GameObject HireDescription, HireStrain, HireFp, HireStress, HireList;
    public GameObject BuyDescription, BuyType, BuyPower, BuyCost;
    public GameObject MenuItem;
    public GameObject ItemName;
    public GameObject ItemCost;
    public GameObject ConfirmPanel;
    public GameObject ConfirmYes;
    public GameObject MessagePanel;
    public GameObject MessageText;
    
    private GuildState currentState;
    private GuildState previousState;
    private GameObject currentItem;
    private GameObject selectedItem;
    private ScrollWithKey scrollview;

    // Start is called before the first frame update
    void Start()
    {
        scrollview = HireCanvas.GetComponent<ScrollWithKey>();
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
                currentState = GuildState.Message;
                UpdateGuildData(currentState);
            }
            else if(currentState == GuildState.Message) {
                currentState = previousState;
            }

            UpdateGuildData(currentState);
        }

        if(currentItem != EventSystem.current.currentSelectedGameObject) {
            currentItem = EventSystem.current.currentSelectedGameObject;
            switch(currentState) {
                case GuildState.Hire:
                    var cData = currentItem.GetComponent<ButtonData>();
                    HireDescription.GetComponent<Text>().text = cData.Description;
                    HireStrain.GetComponent<Text>().text = cData.Strain;
                    HireFp.GetComponent<Text>().text = cData.FocusPoints;
                    HireStress.GetComponent<Text>().text = cData.StressCapacity;
                break;
                case GuildState.Confirm:
                break;
            }
        }
    }

    void UpdateGuildData(GuildState state) {
        ConfirmPanel.SetActive(false);
        MessagePanel.SetActive(false);
        scrollview.ScrollToTop(); //think of generic way instead of acessing all 4 
        switch(state) {
            case GuildState.Hire:
                DestroyChildren(HireList.transform, new List<string>{Constants.MenuTag});
                var yPos = GuildUiConstants.MenuY;
                var index = 0;
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

                    if(index == 0)
                        EventSystem.current.SetSelectedGameObject(pInst);
                    yPos -= GuildUiConstants.MenuYSpacing;
                    index++;
                }
            break;
            case GuildState.Confirm:
                selectedItem = currentItem;
                ConfirmPanel.SetActive(true);
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
        switch(previousState) {
            case GuildState.Hire:
                GameDataSingleton.gameData.PlayerInventory.
                AddPartyMember(GameDataSingleton.gameData.GuildInventory.
                GetCharacterFromId(selectedItem.GetComponent<ButtonData>().Id));
            break;
        }
    }

    private string GetMessageText(GuildState previousState) 
    {
        var name = selectedItem.GetComponentInChildren<Text>().text;
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
