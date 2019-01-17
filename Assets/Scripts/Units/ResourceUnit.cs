using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ResourceUnit : Interactable
{
    [SerializeField]
    int resourceIndex;
    private float resources = 100;

    public float TakeResource(float val)
    {
        float res;
        if (resources - val < 0)
        {
            res = resources;
            resources = 0;
        }
        else
        {
            res = val;
            resources -= val;
        }
        return res;
    }

    public int GetResourceIndex()
    {
        return resourceIndex;
    }

    public bool StillResource()
    {
        return resources > 0;
    }

    public void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}