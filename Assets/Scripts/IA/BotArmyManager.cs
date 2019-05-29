using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

public class BotArmyManager : MonoBehaviour
{
    List<MovableUnit> army = new List<MovableUnit>();

    public void Add(MovableUnit unit)
    {
        if (unit.GetComponent<BuilderUnit>() == null)
            army.Add(unit);
    }

    public void Remove(MovableUnit unit)
    {
        if (army.Contains(unit))
            army.Add(unit);
    }

    public Vector3 GetPosAsHome()
    {
        Vector3 myPos;
        if (GetComponent<IndependantIAManager>() == null)
        {
            ClearList(GetComponent<BotManager>().GetHomes());
            myPos = (GetComponent<BotManager>().GetHomes().Count > 0) ? GetComponent<BotManager>().GetHomes()[0].transform.position : Vector3.zero;
        }
        else
        {
            ClearList(army);
            myPos = (army.Count > 0 && army[0] != null && army[0].GetComponent<MovableUnit>() != null) ? army[0].transform.position : Vector3.zero;
        }
        return myPos;
    }

    void ClearList<T>(List<T> l)
    {
        for (int i = l.Count-1; i >= 0; i--)
        {
            if (l[i] == null)
                l.RemoveAt(i);
        }
    }

    public DestructibleUnit GetNearestEnemy()
    {
        DestructibleUnit res = null;
        List<Holder> enemies = GetEnemyHolders();
        Vector3 myPos = GetPosAsHome();

        foreach (Holder enemy in enemies)
        {
            DestructibleUnit tmp = GetNearestBuildingOf(enemy);
            if (tmp == null) tmp = GetNearestTroopOf(enemy);
            if (res == null || Vector3.Distance(myPos, tmp.transform.position) > Vector3.Distance(myPos, res.transform.position))
                if (tmp != null)
                    res = tmp.GetComponent<DestructibleUnit>();
        }
        return res;
    }

    public DestructibleUnit GetNearestBuildingOf(Holder x)
    {
        DestructibleUnit res = null;
        Transform buildings = x.transform.GetChild(0).Find("Buildings");
        Vector3 myPos = GetPosAsHome();

        foreach (Transform child in buildings)
        {
            if (res == null || Vector3.Distance(myPos, child.position) > Vector3.Distance(myPos, res.transform.position))
                res = child.GetComponent<DestructibleUnit>();
        }
        return res;
    }

    public DestructibleUnit GetNearestTroopOf(Holder x)
    {
        DestructibleUnit res = null;
        Transform buildings = x.transform.GetChild(0).Find("Movable");
        Vector3 myPos = GetPosAsHome();

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

        for (int i = 1; PhotonNetwork.InRoom && i <= PlayerPrefs.GetInt("BotNumber"); i++)
        {
            if (GameObject.Find("Holder" + i).GetComponent<Holder>().team != GetComponent<IAManager>().GetTeam())
                enemies.Add(GameObject.Find("Holder" + i).GetComponent<Holder>());
        }
        return enemies;
    }

    public bool SuicideTroop()
    {
        if (army.Count == 0)
            return false;

        army[0].KillUnit();

        return true;
    }

    public void SendArmy()
    {
        DestructibleUnit target = GetNearestEnemy();

        if (target == null)
            return;

        foreach (MovableUnit troop in army)
        {
            if (troop == null)
                continue;

            troop.Attack(target, false);
            troop.SetAlwaysAttack();
        }
    }

    public void SendArmy(Vector3 pos)
    {
        DestructibleUnit target = GetNearestEnemy();

        if (target == null)
            return;

        foreach (MovableUnit troop in army)
        {
            if (troop == null)
                continue;

            troop.SetAlwaysAttack();
            troop.SetDestination(pos, 2f);
        }
    }

    public void SendArmyMission2(Vector3 pos)
    {
        foreach (MovableUnit troop in army)
        {
            if (troop == null)
                continue;

            troop.SetDestination(pos, 2f);
        }
    }

    public void attackMission2()
    {
        foreach (MovableUnit troop in army)
        {
            if (troop == null)
                continue;
            troop.SetAlwaysAttack();
        }
    }

    public void STOPRUSH()
    {
        foreach (MovableUnit troop in army)
        {
            if (troop == null)
                continue;
            troop.GetComponent<NavMeshAgent>().ResetPath();
        }
    }
}
