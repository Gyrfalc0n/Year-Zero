using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InConstructionUnit : BuildingUnit
{
    string buildingName;

    [SerializeField]
    private float constructionTime;
    float remainingTime;

    public int lines;
    public int columns;

    private List<BuilderUnit> builders = new List<BuilderUnit>();

    public override void Awake()
    {
        base.Awake();
        remainingTime = constructionTime;
    }

    void Update()
    {
        CheckConstruction();
    }

    public void Init(string name)
    {
        buildingName = name.Substring(0, name.Length - 4) + "Unit";
    }

    private void CheckConstruction()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime * builders.Count;
        }
        else
        {
            OnConstructionFinished();
        }
    }

    public virtual void OnConstructionFinished()
    {
        RemoveAllBuilders();
        GameObject obj = PhotonNetwork.Instantiate(buildingName, transform.position, Quaternion.identity);
        InstanceManager.instanceManager.mySelectableObjs.Add(obj.GetComponent<SelectableObj>());
        KillUnit();
    }

    public void AddBuilder(BuilderUnit builder)
    {
        builders.Add(builder);
    }

    public void RemoveBuilder(BuilderUnit builder)
    {
        builders.Remove(builder);
    }

    public override void Cancel()
    {
        //payback
        KillUnit();
    }

    void RemoveAllBuilders()
    {
        for (int i = builders.Count-1; i >= 0; i--)
        {
            builders[i].StopBuild();
        }
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        RemoveAllBuilders();
    }

    public override float GetCurrentActionAdvancement()
    {
        return 1 - remainingTime / constructionTime;
    }
}
