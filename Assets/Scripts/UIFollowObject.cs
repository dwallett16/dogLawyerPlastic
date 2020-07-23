using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform UI;
    public Transform follow;
    public Vector2 offset;

    private void Update ()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(follow.position);
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPos, null, out canvasPos);
        UI.anchoredPosition = canvasPos + offset;
    }
}
