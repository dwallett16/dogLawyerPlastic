using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEvidenceSelectState : BattleState
{
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
                        EventSystem.current?.SetSelectedGameObject(controller.EvidenceButtons[i]);

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
            controller.PlayerActionSelect.NewState = true;
            return controller.PlayerActionSelect;
        }

        return this;
    }
}
