
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollableList : MonoBehaviour
{
    public int itemHeight;
    public int viewRangeStart; //the rect transform posY of the first menu item created under Content panel. Check this while running the scene
    public GameObject ScrollIndicatorUp;
    public GameObject ScrollIndicatorDown;
    public RectTransform PanelMask;
    private int viewRangeEnd;
    private RectTransform contentPanel;
    private IInputManager inputManager;
    private int verticalMovement;
    private int itemsInView;
    private float currentItemY;
    private bool initializedIndicators;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if(!initializedIndicators)
        {
            initializedIndicators = true;
            ScrollIndicatorDown.SetActive(contentPanel.childCount > itemsInView);
        }
        if (EventSystem.current.currentSelectedGameObject == null)
            return;
        verticalMovement = (int)Math.Round(inputManager.GetAxisRaw(Constants.Vertical), 0);
        currentItemY = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y;
        if (verticalMovement != 0)
        {
            if (verticalMovement < 0)
            {
                if (currentItemY < viewRangeEnd)
                {
                    contentPanel.offsetMax -= new Vector2(0, -itemHeight);
                    updateRange(true);
                }
            }
            else if (verticalMovement > 0)
            {
                if (currentItemY > viewRangeStart)
                {
                    contentPanel.offsetMax -= new Vector2(0, itemHeight);
                    updateRange(false);
                }
            }
        }
    }

    void OnEnable()
    {
        Initialize();
        if(contentPanel != null && contentPanel.childCount > 0)
        {
            EventSystem.current.SetSelectedGameObject(contentPanel.GetChild(0).gameObject);
            currentItemY = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y;
            while(currentItemY < viewRangeEnd || currentItemY > viewRangeStart)
            {
                if (currentItemY < viewRangeEnd)
                {
                    contentPanel.offsetMax -= new Vector2(0, -itemHeight);
                    updateRange(true);
                }
                else if (currentItemY > viewRangeStart)
                {
                    contentPanel.offsetMax -= new Vector2(0, itemHeight);
                    updateRange(false);
                }
            }
        }
    }

    public void Initialize()
    {
        contentPanel = GetComponent<RectTransform>();
        inputManager = new InputManager();
        itemsInView = (int)PanelMask.rect.height / itemHeight;
        viewRangeEnd = viewRangeStart - ((itemsInView - 1) * itemHeight);
        ScrollIndicatorUp.SetActive(false);
        ScrollIndicatorDown.SetActive(contentPanel.childCount > itemsInView);
    }

    private void updateRange(bool isShiftingDown)
    {
        var amountToShift = (isShiftingDown ? -itemHeight : itemHeight);
        viewRangeStart += amountToShift;
        viewRangeEnd += amountToShift;
        var firstItemY = contentPanel.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;
        var lastItemY = contentPanel.GetChild(contentPanel.childCount-1).GetComponent<RectTransform>().anchoredPosition.y;

        ScrollIndicatorUp.SetActive(firstItemY > viewRangeStart);
        ScrollIndicatorDown.SetActive(lastItemY < viewRangeEnd);
    }
}
