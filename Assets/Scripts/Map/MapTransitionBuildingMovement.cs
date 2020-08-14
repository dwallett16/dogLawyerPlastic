using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitionBuildingMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(-speed, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
