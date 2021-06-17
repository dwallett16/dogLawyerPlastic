using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuryController : MonoBehaviour
{
    public JuryData JuryData;
    public int NumberOfJurors;
    public int PointsPerJuror;

    // Start is called before the first frame update
    void Start()
    {
        JuryData = new JuryData(NumberOfJurors, PointsPerJuror);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
