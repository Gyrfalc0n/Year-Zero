using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DestructibleUnit : SelectableObj {

    FloatingLifeBarPanel flbPanel;
    FloatingLifeBar lifeBar;

    public override void Awake()
    {
        base.Awake();
        lifeValue = maxLife;
        flbPanel = GameObject.Find("WorldSpaceCanvas").GetComponent<FloatingLifeBarPanel>();
        flbPanel.AddLifeBar(this);
    }

    [SerializeField]
    int maxLife = 5;
    int lifeValue;

    [PunRPC]
    void TakeDamage(int value)
    {
        lifeValue -= value;
        CheckLife();
    }

    private void CheckLife()
    {
        if (photonView.IsMine && lifeValue <= 0)
        {
            KillUnit();
        }
    }

    public void KillUnit()
    {
        if (SelectUnit.selectUnit.selected.Contains(this))
        {
            SelectUnit.selectUnit.selected.Remove(this);
        }
        photonView.RPC("RemoveFromLists", RpcTarget.Others);
        RemoveFromLists();
        if (InstanceManager.instanceManager.mySelectableObjs.Contains(this))
        {
            InstanceManager.instanceManager.mySelectableObjs.Remove(this);
        }
        OnDestroyed();
        PhotonNetwork.Destroy(this.gameObject);
    }

    [PunRPC]
    void RemoveFromLists()
    {
        InstanceManager.instanceManager.allSelectableObjs.Remove(this);
    }

    public virtual void OnDestroyed() { }

    public int GetLife()
    {
        return lifeValue;
    }

    public int GetMaxlife()
    {
        return maxLife;
    }
}
