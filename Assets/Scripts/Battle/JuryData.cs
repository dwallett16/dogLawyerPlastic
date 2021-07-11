using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuryData
{
    public int CurrentPoints { get { return currentPoints; } }
    public int LockedInJurors { 
        get
        {
            return CurrentPoints / pointsPerJuror;
        }
    }
    private int currentPoints;
    private int pointsPerJuror;
    private int totalPoints;
    
    public JuryData(int numJurors, int pointsPerJuror)
    {
        this.pointsPerJuror = pointsPerJuror;
        totalPoints = numJurors * pointsPerJuror;
        currentPoints = 0;
    }

    public void ChangePoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;

        if (currentPoints > totalPoints)
            currentPoints = totalPoints;
        else if (currentPoints < 0)
            currentPoints = 0;
    }
}
