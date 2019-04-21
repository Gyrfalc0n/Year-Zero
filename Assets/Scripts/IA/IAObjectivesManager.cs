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

    public int[] houseAmount;
    public int[] energyFarmAmount;
    public int[] farmAmount;

    void Start()
    {
        forBuilder = false;
        forHouse = false;
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
        else if (currentObjective.GetComponent<ConstructionObjective>() != null && currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 4)
        {
            forHouse = true;
        }
        else if (currentObjective.GetComponent<InstantationObjective>() != null && currentObjective.GetComponent<InstantationObjective>().unitIndex == 2)
        {
            forBuilder = true;
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
                NeedSomething(NeedBuilder);
                break;
            case (ObjectiveState.NeedBuilding):
                NeedSomething(NeedBuilding);
                break;
            case (ObjectiveState.NeedEnergy):
                NeedSomething(NeedEnergy);
                break;
            case (ObjectiveState.NeedOre):
                NeedSomething(NeedOre);
                break;
            case (ObjectiveState.NeedFood):
                NeedSomething(NeedFood);
                break;
            case (ObjectiveState.NeedPop):
                NeedSomething(NeedPop);
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
        for (int i = arrays.arrays[step - 1].objectives.Length-1; i >= 0; i--)
        {
            objectives.Push(CopyObjective(arrays.arrays[step - 1].objectives[i]));
        }
    }

    bool forBuilder;
    bool forHouse;
    void NextObjective()
    {
        if (currentObjective != null)
        {
            if (currentObjective.GetComponent<ConstructionObjective>() != null && currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 4)
                forHouse = false;
            else if (currentObjective.GetComponent<InstantationObjective>() != null && currentObjective.GetComponent<InstantationObjective>().unitIndex == 2)
                forBuilder = false;
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
        if (currentObjective.GetComponent<ConstructionObjective>() != null && currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 4)
        {
            forHouse = true;
        }
        else if (currentObjective.GetComponent<InstantationObjective>() != null && currentObjective.GetComponent<InstantationObjective>().unitIndex == 2)
        {
            forBuilder = true;
        }
    }

    void NextStep()
    {
        GetComponent<BotArmyManager>().SendArmy();
        step++;
        if (step - 1 >= arrays.arrays.Length)
        {
            step--;
        }
        GenerateObjectiveStack();
    }

    void PutBack()
    {
        if (currentObjective == null)
            return;
        currentObjective.Deactivate();
        objectives.Push(currentObjective);
        currentObjective = null;
    }

    IAObjective NeedBuilder()
    {
        PutBack();
        InstantationObjective newObj = Instantiate(instantiationObjectivePrefab, transform);
        newObj.Init(2);
        return newObj;
    }

    IAObjective NeedBuilding()
    {
        int buildingIndex = GetComponent<BotConstructionManager>().GetBuildingIndexFor(currentObjective.GetComponent<InstantationObjective>().unitIndex);
        PutBack();
        ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
        newObj.Init(buildingIndex, forHouse, forBuilder);
        return newObj;
    }

    IAObjective NeedEnergy()
    {
        PutBack();
        if (GetComponent<BotConstructionManager>().GetEnergyFarmCount() < energyFarmAmount[step-1])
        {
            ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
            newObj.Init(2, forHouse, forBuilder);
            return newObj;
        }
        else
        {
            waittingTime = 10f;
            return null;
        }
    }

    IAObjective NeedOre()
    {
        //bool forHouse = (currentObjective.GetComponent<ConstructionObjective>() != null && currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 4);
        PutBack();
        SendToMineObjective newObj = Instantiate(sendToMineObjectivePrefab, transform);
        newObj.Init(1, forHouse, forBuilder);
        return newObj;
    }

    IAObjective NeedFood()
    {
        //bool forHouse = (currentObjective.GetComponent<ConstructionObjective>() != null && (currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 4 ||
            //currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 3));

        PutBack();
        IAObjective newObj;
        if (GetComponent<BotConstructionManager>().GetFarmCount() < farmAmount[step-1])
        {
            newObj = Instantiate(constructionObjectivePrefab, transform).GetComponent<ConstructionObjective>();
            newObj.GetComponent<ConstructionObjective>().Init(3, forHouse, forBuilder);
        }
        else
        {
            newObj = Instantiate(sendToMineObjectivePrefab, transform).GetComponent<SendToMineObjective>();
            newObj.GetComponent<SendToMineObjective>().Init(2, forHouse, forBuilder);
        }
        return newObj;
    }

    IAObjective NeedPop()
    {
        PutBack();
        ConstructionObjective newObj = Instantiate(constructionObjectivePrefab, transform);
        newObj.Init(4, forHouse, forBuilder);
        return newObj;
    }

    void SuicideTroop()
    {
        if (!GetComponent<BotArmyManager>().SuicideTroop())
            PutBack();
        else
            waittingTime = 100;
    }

    IAObjective CopyObjective(IAObjective obj)
    {
        IAObjective newObj;
        if (obj.GetComponent<ConstructionObjective>() != null)
        {
            newObj = Instantiate(constructionObjectivePrefab, transform);
            newObj.GetComponent<ConstructionObjective>().Init((int)obj.GetComponent<ConstructionObjective>().buildingUnits, forHouse, forBuilder);
        }
        else if (obj.GetComponent<InstantationObjective>() != null)
        {
            newObj = Instantiate(instantiationObjectivePrefab, transform);
            newObj.GetComponent<InstantationObjective>().Init((int)obj.GetComponent<InstantationObjective>().unit);
        }
        else
        {
            newObj = null;
            print("wtf");
        }
        return newObj;
    }

    public delegate IAObjective needSTH();

    void NeedSomething(needSTH fun)
    {
        IAObjective tmp = fun();
        if (tmp == null) return;
        currentObjective = tmp;
        currentObjective.Activate();
        if (currentObjective.GetComponent<ConstructionObjective>() != null && currentObjective.GetComponent<ConstructionObjective>().buildingIndex == 4)
        {
            forHouse = true;
        }
        else if (currentObjective.GetComponent<InstantationObjective>() != null && currentObjective.GetComponent<InstantationObjective>().unitIndex == 2)
        {
            forBuilder = true;
        }
    }
}
