using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuryController : MonoBehaviour
{
    private JuryData juryData;
    public int NumberOfJurors;
    public int PointsPerJuror;

    // Start is called before the first frame update
    void Start()
    {
        juryData = new JuryData(NumberOfJurors, PointsPerJuror);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePoints(int points)
    {
        juryData.ChangePoints(points);
    }

    public int GetJuryPoints()
    {
        return juryData.CurrentPoints;
    }

    public void CreateJuryData(int numberOfJurors, int pointsPerJuror)
    {
        juryData = new JuryData(numberOfJurors, pointsPerJuror);
    }
}
