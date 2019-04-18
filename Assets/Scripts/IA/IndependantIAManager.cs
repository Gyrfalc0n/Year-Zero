using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class IndependantIAManager : IAManager
{
    [SerializeField]
    Transform troops;

    void Start()
    {
        SetParameters(-2, 1, -2, 1);
        InitTroops();
    }

    void InitTroops()
    {
        foreach (Transform e in troops)
        {
            e.GetComponent<SelectableObj>().InitUnit(-2);
            GetComponent<BotArmyManager>().Add(e.GetComponent<MovableUnit>());
        }
    }

    public override bool IsEnemy(SelectableObj unit)
    {
        return unit.botIndex == -2;
    }

    public override GameObject InstantiateUnit(string prefab, Vector3 pos, Quaternion rot)
    {
        GameObject obj = PhotonNetwork.Instantiate(prefab, pos, rot);
        obj.transform.SetParent(troops);
        obj.GetComponent<SelectableObj>().InitUnit(-2);
        GetComponent<BotArmyManager>().Add(obj.GetComponent<MovableUnit>());
        return obj;
    }
}
