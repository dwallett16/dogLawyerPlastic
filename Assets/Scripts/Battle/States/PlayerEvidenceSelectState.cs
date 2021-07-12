﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEvidenceSelectState : BattleState
{
    private GameObject topEvidenceItem;
    public override BattleState Execute(BattleController controller)
    {
        if(NewState)
        {
            controller.EvidencePanel.SetActive(true);
            controller.ActionButtonPanel.SetActive(false);
            InitializeState("EvidenceSelect");
            for(int i = 0; i < controller.EvidenceButtons.Count; i++)
            {
                if(i < controller.battleData.EvidenceList.Count)
                {
                    if (i == 0) 
                    {
                        topEvidenceItem = controller.EvidenceButtons[i];
                        EventSystem.current?.SetSelectedGameObject(topEvidenceItem);
                    }

                    controller.EvidenceButtons[i].SetActive(true);
                    controller.EvidenceButtons[i].GetComponentInChildren<Text>().text = controller.battleData.EvidenceList[i].Name;
                    controller.EvidenceButtons[i].GetComponentInChildren<EvidenceButtonData>().EvidenceData = controller.battleData.EvidenceList[i];
                }
                else
                {
                    controller.EvidenceButtons[i].SetActive(false);
                }
            }
        }

        if(controller.IsBackButtonPressed)
        {
            if(controller.EvidenceConfirmPanel.activeInHierarchy) 
            {
                controller.EvidenceConfirmPanel.SetActive(false);
                EventSystem.current?.SetSelectedGameObject(topEvidenceItem);
                return this;
            }
            controller.PlayerActionSelect.NewState = true;
            return controller.PlayerActionSelect;
        }

        if(controller.IsSubmitButtonPressed)
        {
            if(controller.EvidenceConfirmPanel.activeInHierarchy)
            {
                if(controller.MenuConfirmSelection == false)
                {
                    controller.EvidenceConfirmPanel.SetActive(false);
                    EventSystem.current?.SetSelectedGameObject(topEvidenceItem);
                }
                //remove evidence in else statement
            }
            else
            {
                controller.EvidenceConfirmPanel.SetActive(true);
                EventSystem.current?.SetSelectedGameObject(controller.EvidenceConfirmButton);
            }
        }

        return this;
    }
}
