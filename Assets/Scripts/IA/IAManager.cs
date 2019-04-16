using System.Collections.Generic;
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

    public int botIndex { get; private set; }

    public void Init(int index, int race, int team, int color, Vector3 coords)
    {
        SetParameters(index, race, team, color);
        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("SetParameters", RpcTarget.Others, index, race, team, color);
        InitStartingTroops(coords);
    }

    public void InitIndependantTroops(List<int> troops, Vector3 pos)
    {
        BotInstantiationManager m = GetComponent<BotInstantiationManager>();

        foreach (int e in troops)
        {
            InstantiateUnit(m.troopList[e], pos, Quaternion.identity);
        }
    }

    [PunRPC]
    public void SetParameters(int index, int race, int team, int color)
    {
        botIndex = index;
        this.race = race;
        this.team = team;
        this.color = color;
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
        if (botIndex != -2)
        {
            GetComponent<BotManager>().AddHome(InstantiateUnit(townhalls[race], new Vector3(coords.x + 2, 0.5f, coords.z + 2), Quaternion.Euler(0, 0, 0)).GetComponent<TownHall>());
            GetComponent<BotConstructionManager>().InitPos(GetComponent<BotManager>().GetHomes()[0].transform.position);
            InstantiateUnit(builders[race], coords, Quaternion.Euler(0, 0, 0));
        }
    }

    public List<SelectableObj> mySelectableObjs = new List<SelectableObj>();

    public GameObject InstantiateUnit(string prefab, Vector3 pos, Quaternion rot)
    {
        GameObject obj = PhotonNetwork.Instantiate(prefab, pos, rot);
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
        if (PhotonNetwork.OfflineMode)
            return;
        photonView.RPC("RPCAllSelectableRemoveAt", RpcTarget.Others, i);
    }

    public void CleanLists()
    {
        for (int i = mySelectableObjs.Count - 1; i >= 0; i--)
        {
            if (mySelectableObjs[i] == null)
                mySelectableObjs.RemoveAt(i);
        }
    }
}
