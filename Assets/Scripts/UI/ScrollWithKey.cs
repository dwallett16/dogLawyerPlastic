using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollWithKey : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject Content;
    private float percentageToChange;
    private int index;
    private GameObject[] itemList;
    private List<int> scrollIndexes;
    // Start is called before the first frame update
    void Start()
    {
        scrollIndexes = new List<int>();
        
        index = 0;
        itemList = new GameObject[Content.transform.childCount];
        var height = Mathf.Round(ScrollView.GetComponent<RectTransform>().rect.height);
        var numItems = Mathf.Round(height / GuildUiConstants.MenuYSpacing); //how many items fit per page
        var totalItems = Content.transform.childCount;
        percentageToChange = (numItems)/Convert.ToSingle(totalItems);

        for(var l=1; l<Content.transform.childCount; l++) {
            if(l % numItems == 0) {
                scrollIndexes.Add(l);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Vertical")) {
            
            index = Input.GetAxis("Vertical") > 0 ? index-1 : Input.GetAxis("Vertical") < 0 ? index+1 : index;
            if(Input.GetAxis("Vertical") < 0 && index >= 9) { //&& scrollIndexes.Contains(index)) {
                //ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 
                //ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition - percentageToChange < 0 ? 0 : 
                //ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition - percentageToChange;
                ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition -= Convert.ToSingle(.094);
            }
            else if(Input.GetAxis("Vertical") > 0 && index < 10) {
                //ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 
                //ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition + percentageToChange >= 1 ? 1 : 
                //ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition + percentageToChange;
                ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition += Convert.ToSingle(.094);
            }
            Debug.Log(ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition);
        }
    }
}
