using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(TaskSystem))]
public class ConstructedUnit : BuildingUnit
{
    public int lines;
    public int columns;

    public override string GetPath()
    {
        return "Buildings/" + name + "/" + name;
    }

    public string GetConstructorPath()
    {
        return GetPath() + "Cons";
    }

    public GameObject GetGhost()
    {
        return ((GameObject)Resources.Load(GetPath() + "Ghost"));
    }


    float repairTimer;
    List<BuilderUnit> repairers = new List<BuilderUnit>();

    void Update()
    {
        CheckRepair();
    }

    void CheckRepair()
    {
        if (GetLife() < GetMaxlife() && repairers.Count > 0)
        {
            if (repairTimer > 0)
            {
                repairTimer -= Time.deltaTime;
            }
            if (repairTimer <= 0)
            {
                Heal(repairers.Count);
                repairTimer = 1f;
            }
        }
        else
        {
            OnRepairFinished();
        }
    }

    public virtual void OnRepairFinished()
    {
        RemoveAllRepairers();
    }

    public void AddRepairer(BuilderUnit repairer)
    {
        repairers.Add(repairer);
    }

    public void RemoveRepairer(BuilderUnit repairer)
    {
        repairers.Remove(repairer);
    }

    void RemoveAllRepairers()
    {
        for (int i = repairers.Count - 1; i >= 0; i--)
        {
            repairers[i].StopRepairing();
        }
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        RemoveAllRepairers();
    }

}


