using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BotArmyManager : MonoBehaviour
{
    List<MovableUnit> army = new List<MovableUnit>();

    public void Add(MovableUnit unit)
    {
        if (unit.GetComponent<BuilderUnit>() == null)
            army.Add(unit);
    }

    public DestructibleUnit GetNearestEnemy()
    {
        DestructibleUnit res = null;
        List<Holder> enemies = GetEnemyHolders();
        Vector3 myPos = GetComponent<BotManager>().GetHomes()[0].transform.position;

        foreach (Holder enemy in enemies)
        {
            DestructibleUnit tmp = GetNearestBuildingOf(enemy);
            if (res == null || Vector3.Distance(myPos, tmp.transform.position) > Vector3.Distance(myPos, res.transform.position))
                res = tmp.GetComponent<DestructibleUnit>();
        }
        return res;
    }

    public DestructibleUnit GetNearestBuildingOf(Holder x)
    {
        DestructibleUnit res = null;
        Transform buildings = x.transform.GetChild(0).Find("Buildings");
        Vector3 myPos = GetComponent<BotManager>().GetHomes()[0].transform.position;

        foreach (Transform child in buildings)
        {
            if (res == null || Vector3.Distance(myPos, child.position) > Vector3.Distance(myPos, res.transform.position))
                res = child.GetComponent<DestructibleUnit>();
        }
        return res;
    }

    public List<Holder> GetEnemyHolders()
    {
        List<Holder> enemies = new List<Holder>();

        if (!PhotonNetwork.OfflineMode)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (GameObject.Find("Holder" + player.NickName).GetComponent<Holder>().team != GetComponent<IAManager>().GetTeam())
                {
                    enemies.Add(GameObject.Find("Holder" + player.NickName).GetComponent<Holder>());
                }
            }
        }
        else
        {
            if (GameObject.Find("HolderPlayer").GetComponent<Holder>().team != GetComponent<IAManager>().GetTeam())
            {
                enemies.Add(GameObject.Find("HolderPlayer").GetComponent<Holder>());
            }
        }

        for (int i = 1; i < PlayerPrefs.GetInt("BotNumber"); i++)
        {
            if (GameObject.Find("Holder" + i).GetComponent<Holder>().team != GetComponent<IAManager>().GetTeam())
                enemies.Add(GameObject.Find("Holder" + i).GetComponent<Holder>());
        }
        return enemies;
    }
}
