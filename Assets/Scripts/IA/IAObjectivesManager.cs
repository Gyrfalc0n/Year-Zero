using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAObjectivesManager : MonoBehaviour
{
    [SerializeField]
    ConstructionObjective constructionObjectivePrefab;
    [SerializeField]
    InstantationObjective instantiationObjectivePrefab;

    ObjectivesArraysManager arrays;
    ObjectiveArray currentArray;

    List<IAObjective> currentList = new List<IAObjective>();

    float time;
    float timeMax;

    private void Start()
    {
        //To be changed
        arrays = GetComponentInChildren<ObjectivesArraysManager>();
        currentArray = arrays.arrays[0];
        timeMax = 1;
        time = timeMax;
    }

    private void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        if (time <= 0) CheckCurrentObjective();
    }

    void CheckCurrentObjective()
    {
        if (currentList.Count == 0)
            GenerateObjectivesList(currentArray.objectives);
        if (!currentList[0].IsActivated())
            return;
        int result = currentList[0].GetResult();
        if (result == -1)
            return;
        else if (result != 0)
        {
            if (currentList[0].GetComponents<ConstructionObjective>() != null)
            {
                if (result != 2)
                    ReAddTask(currentList[0]);
                NextObjective();
            }
            else if (currentList[0].GetComponents<InstantationObjective>() != null)
            {
                if (result == -2)
                {
                    ConstructionObjective tmp = Instantiate(constructionObjectivePrefab, transform);
                    tmp.Init(4);
                    currentList.Add(tmp);
                }
                else
                {
                    ReAddTask(currentList[0]);
                    NextObjective();
                }
            }
        }
        else
        {
            NextObjective();
        }

    }

    void NextObjective()
    {
        GameObject tmp = currentList[0].gameObject;
        currentList.RemoveAt(0);
        Destroy(tmp);
        time = timeMax;
        if (currentList.Count > 0)
            currentList[0].Activate();
    }

    void LaunchNewList()
    {
        currentList[0].Activate();
    }

    void ReAddTask(IAObjective newObjective)
    {
        IAObjective tmp = CopyObjective(newObjective);
        if (tmp.GetComponent<InstantationObjective>() != null)
        {
            tmp.GetComponent<InstantationObjective>().amount = newObjective.GetComponent<InstantationObjective>().GetResult();
        }
        currentList.Add(tmp);
    }

    void GenerateObjectivesList(IAObjective[] ObjectiveArray)
    {
        currentList.Clear();
        foreach (IAObjective obj in ObjectiveArray)
        {
            currentList.Add(CopyObjective(obj));
        }
        LaunchNewList();
    }

    IAObjective CopyObjective(IAObjective objective)
    {
        IAObjective obj = null;
        if (objective.GetComponent<ConstructionObjective>() != null)
        {
            ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
            ConstructionObjective model = objective.GetComponent<ConstructionObjective>();
            newObj.Init(model.buildingIndex);
            obj = newObj;
        }
        else if (objective.GetComponent<InstantationObjective>() != null)
        {
            InstantationObjective newObj = Instantiate(instantiationObjectivePrefab, transform);
            InstantationObjective model = objective.GetComponent<InstantationObjective>();
            newObj.Init(model.unitIndex, model.amount);
            obj = newObj;
        }
        else
        {
            print("wtf");
        }
        obj.transform.SetParent(transform);
        return obj.GetComponent<IAObjective>();
    }
}
