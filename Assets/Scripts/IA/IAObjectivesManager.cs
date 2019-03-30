using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAObjectivesManager : MonoBehaviour
{
    [SerializeField]
    ConstructionObjective constructionObjectivePrefab;
    [SerializeField]
    InstantationObjective instantiationObjectivePrefab;
    float waittingTime;
    [SerializeField]
    SendToMineObjective sendToMineObjectivePrefab;

    public int step { get; private set; }

    public IAObjective currentObjective { get; private set; }
    public ObjectivesArraysManager arrays;
    Stack<IAObjective> objectives = new Stack<IAObjective>();

    bool stop;

    void Start()
    {
        step = 1;
        stop = false;
        waittingTime = 0;
        GenerateObjectiveStack();
        currentObjective = objectives.Pop();
        currentObjective.Activate();
    }

    void Update()
    {
        if (waittingTime <= 0)
            Check();
        else
            waittingTime -= Time.deltaTime;
    }

    void Check()
    {
        if (stop) return;

        if (currentObjective == null)
        {
            NextObjective();
        }
        switch (currentObjective.state)
        {
            case (ObjectiveState.Deactivated):
                print("wtf");
                break;
            case (ObjectiveState.Activated):
                break;
            case (ObjectiveState.Done):
                NextObjective();
                break;
            case (ObjectiveState.NeedBuilder):
                NeedBuilder();
                break;
            case (ObjectiveState.NeedBuilding):
                NeedBuilding();
                break;
            case (ObjectiveState.NeedEnergy):
                NeedEnergy();
                break;
            case (ObjectiveState.NeedOre):
                NeedOre();
                break;
            case (ObjectiveState.NeedFood):
                NeedFood();
                break;
            case (ObjectiveState.NeedPop):
                NeedPop();
                break;
            case (ObjectiveState.NeedWait):
                waittingTime = 10;
                break;
            case (ObjectiveState.SuicideTroop):
                SuicideTroop();
                break;
        }
    }

    void GenerateObjectiveStack()
    {
        foreach (IAObjective obj in arrays.arrays[step - 1].objectives)
        {
            objectives.Push(obj);
        }
    }

    void NextObjective()
    {
        if (currentObjective != null)
        {
            Destroy(currentObjective.gameObject);
        }

        if (objectives.Count > 0)
        {
            currentObjective = objectives.Pop();
            currentObjective.Activate();
        }
        else
        {
            NextStep();
        }
    }

    void NextStep()
    {
        GetComponent<BotArmyManager>().SendArmy();
        step++;
        if (step - 1 >= arrays.arrays.Length)
        {
            print("Finished!");
            stop = true;
        }

        else
            GenerateObjectiveStack();
    }

    void PutBack()
    {
        currentObjective.Deactivate();
        objectives.Push(currentObjective);
        currentObjective = null;
    }

    void NeedBuilder()
    {
        PutBack();
        InstantationObjective newObj = Instantiate(instantiationObjectivePrefab, transform);
        newObj.Init(2);
        currentObjective = newObj;
        currentObjective.Activate();
    }

    void NeedBuilding()
    {
        int buildingIndex = GetComponent<BotConstructionManager>().GetBuildingIndexFor(currentObjective.GetComponent<InstantationObjective>().unitIndex);
        PutBack();
        ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
        newObj.Init(buildingIndex);
        currentObjective = newObj;
        currentObjective.Activate();
    }

    void NeedEnergy()
    {
        PutBack();
        if (GetComponent<BotConstructionManager>().GetEnergyFarmCount() < GetComponent<IAObjectivesManager>().step + 2 * GetComponent<IAObjectivesManager>().step)
        {
            ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
            newObj.Init(2);
            currentObjective = newObj;
            currentObjective.Activate();
        }
        else
        {
            waittingTime = 10f;
        }
    }

    void NeedPop()
    {
        PutBack();
        ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
        newObj.Init(4);
        currentObjective = newObj;
        currentObjective.Activate();
    }

    void SuicideTroop()
    {
        if (!GetComponent<BotArmyManager>().SuicideTroop())
            PutBack();
        else
            waittingTime = 100;
    }

    void NeedOre()
    {
        PutBack();
        SendToMineObjective newObj = Instantiate(sendToMineObjectivePrefab, transform);
        newObj.Init(1);
        currentObjective = newObj;
        currentObjective.Activate();
    }

    void NeedFood()
    {
        PutBack();
        IAObjective newObj;
        if (GetComponent<BotConstructionManager>().GetFarmCount() < GetComponent<IAObjectivesManager>().step + 2 * GetComponent<IAObjectivesManager>().step)
        {
            newObj = Instantiate(constructionObjectivePrefab, transform).GetComponent<ConstructionObjective>();
            newObj.GetComponent<ConstructionObjective>().Init(3);
        }
        else
        {
            newObj = Instantiate(sendToMineObjectivePrefab, transform).GetComponent<SendToMineObjective>();
            newObj.GetComponent<SendToMineObjective>().Init(2);
        }
        currentObjective = newObj;
        currentObjective.Activate();

    }
}
