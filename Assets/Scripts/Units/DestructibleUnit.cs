using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class DestructibleUnit : SelectableObj {

    FloatingLifeBarPanel flbPanel;
    FloatingLifeBar lifeBar;

    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        lifeValue = maxLife;
        flbPanel = GameObject.Find("WorldSpaceCanvas").GetComponent<FloatingLifeBarPanel>();
        flbPanel.AddLifeBar(this);
    }

    public int defaultMaxLife;
    [HideInInspector]
    public int maxLife;
    int lifeValue;

    public Sprite iconSprite;

    public void TakeDamage(int value, DestructibleUnit shooter)
    {
        RPCTakeDamage(value);
        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("RPCTakeDamage", RpcTarget.Others, value);
        }
        CheckLife();
        OnDamageTaken(shooter);
    }

    public virtual void OnDamageTaken(DestructibleUnit shooter) { }

    [PunRPC]
    public void RPCTakeDamage(int value)
    {
        lifeValue -= value;
    }

    public void SetLife(float val)
    {
        lifeValue = (int)val;
    }

    public void Heal(int value)
    {
        RPCHeal(value);
        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("RPCHeal", RpcTarget.Others, value);
    }

    [PunRPC]
    public void RPCHeal(int value)
    {
        lifeValue += value;
        if (lifeValue > maxLife)
            lifeValue = maxLife;
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
        InstanceManager.instanceManager.AllSelectableRemoveAt(InstanceManager.instanceManager.allSelectableObjs.IndexOf(this));
        if (SelectUnit.selectUnit.selected.Contains(this))
        {
            SelectUnit.selectUnit.selected.Remove(this);
        }
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

    public float GetLife()
    {
        return lifeValue;
    }

    public int GetMaxlife()
    {
        return maxLife;
    }

    public virtual bool IsAvailable()
    {
        return true;
    }
}
