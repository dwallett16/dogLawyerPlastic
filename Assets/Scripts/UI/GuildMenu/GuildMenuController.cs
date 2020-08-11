using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildMenuController : MonoBehaviour
{
    public GameObject HireDescription, HireStrain, HireFp, HireStress;
    public GameObject BuyDescription, BuyType, BuyPower, BuyCost;
    public GameObject GameData;
    
    private GuildState currentState;
    private GameObject currentItem;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GuildState.Hire;
        
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
