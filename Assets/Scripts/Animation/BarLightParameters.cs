using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLightParameters : MonoBehaviour
{
    public float CycleOffset = 0f;
    public float SpeedMultiplier = 1f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("CycleOffset", CycleOffset);
        animator.SetFloat("SpeedMultiplier", SpeedMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
