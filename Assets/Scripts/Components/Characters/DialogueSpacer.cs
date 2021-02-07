
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogueSpacer : MonoBehaviour
{
    public float DialogueRange;
    // Start is called before the first frame update
    void Start()
    {
        var pos = this.gameObject.transform.position;
        var emptyObj = new GameObject("Empty");
        var left = Instantiate(emptyObj, new Vector3(pos.x-DialogueRange, pos.y, pos.z), Quaternion.identity, gameObject.transform);
        var right = Instantiate(emptyObj, new Vector3(pos.x+DialogueRange, pos.y, pos.z), Quaternion.identity, gameObject.transform);
        var actor = this.gameObject.transform.Find("DialogueTrigger").GetComponent<DialogueActor>().actor;
        left.name = actor + "LeftPoint";
        right.name = actor + "RightPoint";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
