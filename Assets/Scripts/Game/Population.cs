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

    public void Add(int val)
    {
        value += val;
    }

    public void AddMax(int val)
    {
        rawValue += val;
        currentMaxValue += val;
        if (currentMaxValue > maxValue)
            currentMaxValue = maxValue;
    }

    public void Remove(int val)
    {
        value -= val;
    }

    public void RemoveMax(int val)
    {
        rawValue -= val;
        if (rawValue < maxValue)
            currentMaxValue = rawValue;
    }
}
