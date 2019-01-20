﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AsteroidResourceUnit : ResourceUnit
{
    void Awake()
    {
        resourceIndex = 1;
        SetResources(Random.Range(500, 2000));
    }

    public override void OnNoMoreResources()
    {
        Destroy();
    }
}
