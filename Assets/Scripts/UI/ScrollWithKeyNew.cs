using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollWithKeyNew : MonoBehaviour
{
    int Index;
    int MaxIndex;
    bool isPressUp, isPressDown, isPressConfirm;
    [SerializeField] 
    bool isKeyPressed;
    RectTransform rectTransform;
    IInputManager inputManager;
    int verticalMovement;
    int itemSize = 30;
    int viewRangeStart = 0;
    int viewRangeEnd = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        inputManager = new InputManager();
        MaxIndex = transform.childCount;
        Debug.Log("Max index: " + MaxIndex);
        viewRangeStart = 120;
        viewRangeEnd = -120;
    }

    // Update is called once per frame
    void Update()
    {
        //verticalMovement = inputManager.GetButtonDown(Constants.Vertical) ? (int)Math.Round(inputManager.GetAxisRaw(Constants.Vertical), 0) : 0;
        verticalMovement = (int)Math.Round(inputManager.GetAxisRaw(Constants.Vertical), 0);
        var mouseWheelMovement = Input.mouseScrollDelta.y;
        //if(verticalMovement == 0 && mouseWheelMovement != 0)
        //{
        //    verticalMovement = (int)mouseWheelMovement;
        //}
        var currentItemY = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().localPosition.y;
        if (verticalMovement != 0)
        {
            //if (!isKeyPressed)
            //{
                if (verticalMovement < 0)
                {
                    //if (Index < MaxIndex)
                    //{
                        Index++;
                        if (currentItemY < viewRangeEnd)
                        {
                           rectTransform.offsetMax -= new Vector2(0, -itemSize);
                           updateRange(true);
                        }
                    //}
                    //else
                    //{
                        //rectTransform.offsetMax = Vector2.zero;
                    //}

                }
                else if (verticalMovement > 0)
                {

                    //if (Index > 0)
                    //{
                        Index--;
                        if (currentItemY > viewRangeStart)
                        {
                           rectTransform.offsetMax -= new Vector2(0, itemSize);
                           updateRange(false);
                        }
                    //}
                    //else
                    //{
                        //8 comes from- (maxIndex - (visibleOptions - 1)) * height 
                        //rectTransform.offsetMax = new Vector2(0, (MaxIndex - 8) * itemSize);
                    //}
                }
                isKeyPressed = true;
            //}
        }
        else
        {
            isKeyPressed = false;
        }
        Debug.Log(Input.mouseScrollDelta.y);
    }

    private void updateRange(bool isShiftingDown)
    {
        var amountToShift = (isShiftingDown ? -itemSize : itemSize) / 2;
        viewRangeStart += amountToShift;
        viewRangeEnd += amountToShift;
        Debug.Log("new range" + viewRangeStart + ": " + viewRangeEnd);
    }

    public void onPressUp()
    {
        isPressUp = true;
    }

    public void onReleaseUp()
    {
        isPressUp = false;
    }

    public void onPressDown()
    {
        isPressDown = true;
    }

    public void onReleaseDown()
    {
        isPressDown = false;
    }

    public void onPressConfirm()
    {
        isPressConfirm = true;
    }

    public void onReleaseConfirm()
    {
        isPressConfirm = false;
    }
}
