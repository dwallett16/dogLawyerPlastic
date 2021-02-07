using UnityEngine;

public class PlayerControllerDisabler : MonoBehaviour, IComponentDisabler
{
    public void ToggleComponent() {
        var controller = GetComponent<PlayerController>();
        controller.enabled = !controller.enabled;
    }
}
