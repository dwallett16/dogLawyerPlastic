using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float SpeedTimer = 4.0f;
    public float Speed = 20;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(finished)
            return;
        SpeedTimer -= Time.deltaTime;
        if(SpeedTimer <= 0) {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
            finished = true;
        }
    }
}
