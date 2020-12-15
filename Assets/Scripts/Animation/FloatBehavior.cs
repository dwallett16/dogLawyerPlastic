using System;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    float originalY;

    public float FloatStrength = 1;
    public float Frequency = 1;

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin((Time.time * Math.PI * Frequency)) * FloatStrength),
            transform.position.z);
    }
}
