using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollWithKey : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject Content;
    public GameObject ListItem;
    private float percentageToScroll;
    private int index;
    private float numItems;
    private GameObject currentItem;
    // Start is called before the first frame update
    void Start()
    {
        currentItem = EventSystem.current.currentSelectedGameObject;
        index = 0;
        var height = Mathf.Round(ScrollView.GetComponent<RectTransform>().rect.height);
        numItems = Mathf.Round(height / ListItem.GetComponent<RectTransform>().rect.height); //how many items fit per page
        var totalItems = Content.transform.childCount;
        percentageToScroll = 1/(Convert.ToSingle(totalItems)-numItems);
    }

    // Update is called once per frame
    void Update()
    {
        var itemsPerPage = Convert.ToInt32(numItems);
        if(Input.GetAxis(Constants.Vertical) < 0) {
            if(currentItem != EventSystem.current.currentSelectedGameObject) {
                currentItem = EventSystem.current.currentSelectedGameObject;
                index = index + 1 > itemsPerPage ? itemsPerPage : index + 1;
                if(index == itemsPerPage) {
                    ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 
                    ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition - percentageToScroll < 0 ? 0 : 
                    ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition - percentageToScroll;
                }
            }
        }
        else if(Input.GetAxis(Constants.Vertical) > 0) {
            if(currentItem != EventSystem.current.currentSelectedGameObject) {
                currentItem = EventSystem.current.currentSelectedGameObject;
                index = index - 1 < 0 ? 0 : index - 1;
                if(index == 0) {
                    ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 
                    ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition + percentageToScroll >= 1 ? 1 : 
                    ScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition + percentageToScroll;
                }
            }
        }
    }
}
