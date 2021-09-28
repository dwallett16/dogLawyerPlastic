using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityHelper : IProbabilityHelper
{
    public int GenerateNumberInRange(int startRange, int endRange)
    {
        return Random.Range(startRange, endRange);
    }
}
