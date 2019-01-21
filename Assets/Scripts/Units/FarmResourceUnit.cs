using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmResourceUnit : ResourceUnit
{
    float maxResources = 100;
    float add = 0.1f;

    void Awake()
    {
        resourceIndex = 2;
        SetResources(0);
    }

    void Update()
    {
        resources = (resources + add > maxResources) ? maxResources : resources +add;
    }
}
