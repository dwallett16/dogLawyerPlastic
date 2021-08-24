
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollWithKeyNew : MonoBehaviour
{
    public int itemHeight;
    public int viewRangeStart; //the rect transform posY of the first menu item created under Content panel. Check this while running the scene
    private int viewRangeEnd;
    public RectTransform PanelMask;
    private RectTransform contentPanel;
    private IInputManager inputManager;
    private int verticalMovement;
    private int itemsInView;
    private float currentItemY;

    void Start()
    {
        contentPanel = GetComponent<RectTransform>();
        inputManager = new InputManager();
        itemsInView = (int)PanelMask.rect.height / itemHeight;
        viewRangeEnd = viewRangeStart - ((itemsInView - 1) * itemHeight);
    }

    void Update()
    {
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

    private void updateRange(bool isShiftingDown)
    {
        var amountToShift = (isShiftingDown ? -itemHeight : itemHeight);
        viewRangeStart += amountToShift;
        viewRangeEnd += amountToShift;
    }
}
