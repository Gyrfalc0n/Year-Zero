using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Population {

    private int rawValue;
    private int maxValue = 100;
    private int currentMaxValue = 0;
    private int value;

    public int GetCurrentMaxValue()
    {
        return currentMaxValue;
    }

    public int GetValue()
    {
        return value;
    }

    public int Add(int val)
    {
        value += val;
        return value;
    }

    public int AddMax(int val)
    {
        rawValue += val;
        currentMaxValue += val;
        if (currentMaxValue > maxValue)
            currentMaxValue = maxValue;
        return value;
    }

    public int Remove(int val)
    {
        rawValue -= val;
        if (rawValue < val)
            val = rawValue;
        return value;
    }
}
