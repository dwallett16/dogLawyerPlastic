
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollWithKeyNew : MonoBehaviour
{
    public int itemSize;
    public int viewRangeStart; //the rect transform posY of the first menu item under Content panel. You can check this while running the scene
    private int viewRangeEnd;
    public RectTransform PanelMask;
    private RectTransform contentPanel;
    private IInputManager inputManager;
    private int verticalMovement;
    private int itemsInView;

    void Start()
    {
        contentPanel = GetComponent<RectTransform>();
        inputManager = new InputManager();
        itemsInView = (int)PanelMask.rect.height / itemSize;
        viewRangeEnd = viewRangeStart - ((itemsInView - 1) * itemSize);
    }

    // Update is called once per frame
    void Update()
    {
        verticalMovement = (int)Math.Round(inputManager.GetAxisRaw(Constants.Vertical), 0);
        var mouseWheelMovement = Input.mouseScrollDelta.y;
        var currentItemY = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y;
        if (verticalMovement != 0)
        {
            if (verticalMovement < 0)
            {
                if (currentItemY < viewRangeEnd)
                {
                    contentPanel.offsetMax -= new Vector2(0, -itemSize);
                    updateRange(true);
                }
            }
            else if (verticalMovement > 0)
            {
                if (currentItemY > viewRangeStart)
                {
                    contentPanel.offsetMax -= new Vector2(0, itemSize);
                    updateRange(false);
                }
            }
        }
    }

    private void updateRange(bool isShiftingDown)
    {
        var amountToShift = (isShiftingDown ? -itemSize : itemSize);
        viewRangeStart += amountToShift;
        viewRangeEnd += amountToShift;
    }
}
