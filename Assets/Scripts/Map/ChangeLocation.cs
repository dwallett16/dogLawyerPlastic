using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeLocation : MonoBehaviour
{
    public GameObject AssociatedBuilding;
    public Material HighlightedMaterial;
    private Material originalMaterial;
    private bool revert = false;

    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = AssociatedBuilding.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject == EventSystem.current.currentSelectedGameObject) {
            AssociatedBuilding.GetComponent<MeshRenderer>().material = HighlightedMaterial;
            revert = true;
        }
        else if(revert){
            AssociatedBuilding.GetComponent<MeshRenderer>().material = originalMaterial;
            revert = false;
        }
    }
}
