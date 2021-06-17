using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuryData
{
    public int CurrentPoints { get { return currentPoints; } }
    public int LockedInJurors { 
        get
        {
            return CurrentPoints / PointsPerJuror;
        }
    }
    private int currentPoints;
    private int NumberOfJurors;
    private int PointsPerJuror;
    private int TotalPoints;
    
    public JuryData(int numJurors, int pointsPerJuror)
    {
        NumberOfJurors = numJurors;
        PointsPerJuror = pointsPerJuror;
        TotalPoints = numJurors * pointsPerJuror;
        currentPoints = 0;
    }

    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
    }

    public void RemovePoints(int pointsToSubtract)
    {
        var minPoints = LockedInJurors * PointsPerJuror;
        currentPoints += pointsToSubtract;
        if (currentPoints < minPoints)
            currentPoints = minPoints;
    }
}
