using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameResource {

    string name;
    private int value;

    public GameResource(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
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

    public int Remove(int val)
    {
        value -= val;
        if (value < 0)
            value = 0;
        return value;
    }
}
