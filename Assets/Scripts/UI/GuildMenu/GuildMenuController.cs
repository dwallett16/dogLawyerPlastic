using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildMenuController : MonoBehaviour
{
    public GameObject HireCanvas, BuyCanvas, FireCanvas, SellCanvas;
    public GameObject HireDescription, HireStrain, HireFp, HireStress, HireMenu;
    public GameObject BuyDescription, BuyType, BuyPower, BuyCost;
    public GameObject MenuItem;
    public GameObject ItemName;
    public GameObject ItemCost;
    
    private GuildState currentState;
    private GameObject currentItem;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GuildState.Hire;
        DestroyChildren(HireCanvas.transform, new List<string>{Constants.MenuTag});
        var yPos = GuildUiConstants.MenuY;
        var index = 0;
        foreach(var p in GameDataSingleton.gameData.GuildInventory.PartyList) {
            var pInst = Instantiate(MenuItem, Vector3.zero, Quaternion.identity, HireMenu.transform);
            pInst.GetComponent<RectTransform>().anchoredPosition = new Vector2(GuildUiConstants.MenuX, yPos);
            pInst.name = pInst.name + index;
            var pName = Instantiate(ItemName, Vector3.zero, Quaternion.identity, pInst.transform);
            pName.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            pName.GetComponent<Text>().text = p.Name;
            var pCost = Instantiate(ItemCost, Vector3.zero, Quaternion.identity, pInst.transform);
            pCost.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            pCost.GetComponent<Text>().text = p.Price.ToString() + "f";

            if(index == 0)
                EventSystem.current.SetSelectedGameObject(pInst);
            yPos -= GuildUiConstants.MenuYSpacing;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
