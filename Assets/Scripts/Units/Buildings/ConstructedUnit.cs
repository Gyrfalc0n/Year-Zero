using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(TaskSystem))]
public class ConstructedUnit : BuildingUnit
{
    [SerializeField]
    InConstructionUnit constructor;
    [SerializeField]
    GameObject ghost;

    public InConstructionUnit GetConstructor()
    {
        return constructor;
    }

    public GameObject GetGhost()
    {
        return ghost;
    }
}


