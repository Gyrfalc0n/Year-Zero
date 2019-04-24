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
        lifeValue = defaultMaxLife;
        maxLife = defaultMaxLife;
        if (!PhotonNetwork.OfflineMode)
        {
            photonView.RPC("RPCInitDestructible", RpcTarget.Others, lifeValue, maxLife);
        }
        flbPanel = GameObject.Find("WorldSpaceCanvas").GetComponent<FloatingLifeBarPanel>();
        flbPanel.AddLifeBar(this);
    }

    [PunRPC]
    public override void RPCInitUnit(int botIndex)
    {
        base.RPCInitUnit(botIndex);
        flbPanel = GameObject.Find("WorldSpaceCanvas").GetComponent<FloatingLifeBarPanel>();
        flbPanel.AddLifeBar(this);
    }

    [PunRPC]
    public void RPCInitDestructible(int lifeValue, int maxLife)
    {
        this.lifeValue = lifeValue;
        this.maxLife = maxLife;
    }

    public int defaultMaxLife;
    [HideInInspector]
    public int maxLife;
    int lifeValue;

    public Sprite iconSprite;

    public void TakeDamage(int value, DestructibleUnit shooter)
    {
        if (photonView.IsMine)
            RPCTakeDamage(value);
        else
        {
            photonView.RPC("RPCTakeDamage", RpcTarget.Others, value);
        }
        //OnDamageTaken(shooter);
    }

    public virtual void OnDamageTaken(DestructibleUnit shooter) { }

    [PunRPC]
    public void RPCTakeDamage(int value)
    {
        lifeValue -= value;
        if (photonView.IsMine)
            CheckLife();
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
        if (botIndex == -1)
            InstanceManager.instanceManager.CheckDeath();
        else if (botIndex != -2)
            InstanceManager.instanceManager.GetBot(botIndex).CheckDeath();
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
