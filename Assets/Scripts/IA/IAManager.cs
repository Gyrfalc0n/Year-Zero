﻿using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class IAManager : MonoBehaviourPunCallbacks
{
    int race;
    int team;
    int color;

    string[] townhalls = new string[2] { "Buildings/TownHall/TownHall", "Buildings/TownHall/TownHall" };
    string[] builders = new string[2] { "Units/Builder", "Units/Builder" };

    int botIndex;

    public Transform movableUnits;
    public Transform buildingUnits;

    public void Init(int index, int race, int team, int color, Vector3 coords)
    {
        botIndex = index;
        this.race = race;
        this.team = team;
        this.color = color;
        InitStartingTroops(coords);
    }

    void CheckDeath()
    {
        if (mySelectableObjs.Count == 0)
        {
            Debug.Log("You're Dead");
        }
    }

    void InitStartingTroops(Vector3 coords)
    {
        GetComponent<BotManager>().AddHome(InstantiateUnit(townhalls[race], new Vector3(coords.x + 2, 0.5f, coords.z + 2), Quaternion.Euler(0, 0, 0)).GetComponent<TownHall>());
        InstantiateUnit(builders[race], coords, Quaternion.Euler(0, 0, 0));
    }


    public List<SelectableObj> allSelectableObjs = new List<SelectableObj>();
    public List<SelectableObj> mySelectableObjs = new List<SelectableObj>();

    public GameObject InstantiateUnit(string prefab, Vector3 pos, Quaternion rot)
    {
        GameObject obj = PhotonNetwork.Instantiate(prefab, pos, rot);
        if (obj.GetComponent<MovableUnit>() != null)
            obj.transform.SetParent(movableUnits);
        else
            obj.transform.SetParent(buildingUnits);
        obj.GetComponent<SelectableObj>().InitUnit(botIndex);
        return obj;
    }

    public int GetTeam()
    {
        return team;
    }

    public int GetRace()
    {
        return race;
    }

    public Color32 GetColor()
    {
        return Int2Color(color);
    }

    public Color32 GetPlayerColor(Player player)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            return Int2Color(color);
        }
        return Int2Color((int)player.CustomProperties["Color"]);
    }

    public Color32 GetBotColor(int index)
    {
        return GameObject.Find("Bot" + index).GetComponent<IAManager>().GetColor();
    }


    public Color32 Int2Color(int val)
    {
        Color32 res;
        if (val == 0)
        {
            res = new Color32(255, 0, 0, 255);
        }
        else if (val == 1)
        {
            res = new Color32(0, 255, 0, 255);
        }
        else
        {
            res = new Color32(0, 0, 255, 255);
        }
        return res;
    }

    public bool IsEnemy(SelectableObj unit)
    {
        if (unit.botIndex == -1)
        {
            if (PhotonNetwork.OfflineMode)
            {
                return InstanceManager.instanceManager.GetTeam() != GetTeam();
            }
            else
            {
                return (int)unit.photonView.Owner.CustomProperties["Team"] != GetTeam();
            }
        }
        else
        {
            return (GetTeam() != InstanceManager.instanceManager.GetBot(unit.botIndex).GetTeam());
        }
    }

    public void AllSelectableRemoveAt(int i)
    {
        photonView.RPC("RPCAllSelectableRemoveAt", RpcTarget.Others, i);
    }

    [PunRPC]
    public void RPCAllSelectableRemoveAt(int i)
    {
        allSelectableObjs.RemoveAt(i);
    }


    #region BotTools

    Vector3 lastBuildingPos = Vector3.zero;
    public void Construct(ConstructedUnit building, BuilderUnit builder)
    {
        Vector3 newPos = GenerateNewPos();
        GameObject obj = InstanceManager.instanceManager.InstantiateUnit(building.GetConstructorPath(), newPos, Quaternion.identity);
        obj.GetComponent<InConstructionUnit>().Init(building);
        GetComponent<BotManager>().Pay(building.costs, building.pop);
        builder.Build(obj.GetComponent<InConstructionUnit>());
    }

    //int currentSize = 1;
    public Vector3 GenerateNewPos()
    { 
        return lastBuildingPos;
    }

    #endregion
}
