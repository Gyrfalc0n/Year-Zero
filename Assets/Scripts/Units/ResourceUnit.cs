using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ResourceUnit : Interactable, IPunObservable
{
    protected int resourceIndex;
    protected float resources;

    void Start()
    {
        InstanceManager.instanceManager.allResourceUnits.Add(this);
    }

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

    public virtual void OnNoMoreResources() { }

    protected void SetResources(float value)
    {
        resources = value;
    }

    protected void Destroy()
    {
        if (photonView.IsMine)
        {
            InstanceManager.instanceManager.AllResourceUnitsRemoveAt(InstanceManager.instanceManager.allResourceUnits.IndexOf(this));
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(resources);
        }
        else
        {
            resources = (float)stream.ReceiveNext();
        }
    }
}