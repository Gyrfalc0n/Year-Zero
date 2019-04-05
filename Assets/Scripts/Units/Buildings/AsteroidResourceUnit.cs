using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AsteroidResourceUnit : ResourceUnit
{
    void Awake()
    {
        resourceIndex = 1;
        SetResources(Random.Range(50000, 200000));
    }

    public override void OnNoMoreResources()
    {
        Destroy();
    }
}
