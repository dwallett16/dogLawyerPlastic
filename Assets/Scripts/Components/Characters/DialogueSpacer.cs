
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogueSpacer : MonoBehaviour
{
    public float DialogueRange;
    public bool IsFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        if(DialogueRange == 0)
            Debug.LogError("No range is set for this character spacing. See DialogueSpacer component and increase dialogue range.");

        var pos = this.gameObject.transform.position;
        var emptyObj = new GameObject("Empty");
        var left = Instantiate(emptyObj, new Vector3(pos.x-DialogueRange, pos.y, pos.z), Quaternion.identity, gameObject.transform);
        var right = Instantiate(emptyObj, new Vector3(pos.x+DialogueRange, pos.y, pos.z), Quaternion.identity, gameObject.transform);
        var actor = this.gameObject.transform.Find("DialogueTrigger").GetComponent<DialogueActor>().actor;
        left.name = actor + "LeftPoint";
        right.name = actor + "RightPoint";

        var directionX = IsFacingRight ? pos.x + DialogueRange : pos.x = DialogueRange;
        var directionVector = new Vector3(directionX, pos.y, pos.z);
        var facingPoint = Instantiate(emptyObj, directionVector, Quaternion.identity, gameObject.transform);
        facingPoint.name = actor + "FacingPoint";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
