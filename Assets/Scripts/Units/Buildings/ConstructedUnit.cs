using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(TaskSystem))]
public class ConstructedUnit : BuildingUnit
{
    [SerializeField]
    InConstructionUnit constructor;
    [SerializeField]
    GameObject ghost;

    public int lines;
    public int columns;

    public InConstructionUnit GetConstructor()
    {
        return constructor;
    }

    public GameObject GetGhost()
    {
        return ghost;
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


