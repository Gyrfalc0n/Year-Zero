using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBuilderManager : MonoBehaviour
{
    List<BuilderUnit> builders = new List<BuilderUnit>();
    BotManager m;

    private void Start()
    {
        m = GetComponent<BotManager>();
    }

    public void Add(BuilderUnit newBuilder)
    {
        builders.Add(newBuilder);
    }

    public void Remove(BuilderUnit builder)
    {
        builders.Remove(builder);
    }

    public ObjectiveState GetOneBuilder(out BuilderUnit builder, bool forHouse, bool forBuilder, int toMine=-1)
    {
        builder = GetJoblessBuilder();
        if (builder != null)
            return ObjectiveState.Activated;

        if ((float)builders.Count >= m.GetMaxPopulation() || (float)builders.Count/m.GetMaxPopulation()*100 >= 25)
        {
            float inConstruction = InConstructionBuilders();
            if (inConstruction == builders.Count || inConstruction/builders.Count*10 > 35)
            {
                return ObjectiveState.NeedWait;
            }
            else
            {
                return TakeOrHouse(out builder, forHouse, toMine, forBuilder);
            }
        }
        else
        {
            if (m.GetPopulation() == m.GetMaxPopulation() || forBuilder)
            {
                return TakeOrHouse(out builder, forHouse, toMine, forBuilder);
            }
            else
            {
                return ObjectiveState.NeedBuilder;
            }
        }
    }

    ObjectiveState TakeOrHouse(out BuilderUnit builder, bool forHouse, int toMine, bool forBuilder)
    {
        if (GetComponent<BotConstructionManager>().GetHouseCount() < GetComponent<IAObjectivesManager>().houseAmount[GetComponent<IAObjectivesManager>().step-1])
        {
            if (forHouse)
            {
                return TakeBuilder(out builder, forHouse, toMine);
            }
            else
            {
                builder = null;
                return ObjectiveState.NeedPop;
            }
        }
        else
        {
            if (toMine != -1 && MiningBuilders() > 1 && Mathf.Abs(MiningBuilders(1) - MiningBuilders(2)) <= 1)
            {
                builder = null;
                return ObjectiveState.Activated;
            }
            return TakeBuilder(out builder, forHouse, toMine);
        }
    }

    ObjectiveState TakeBuilder(out BuilderUnit builder, bool forHouse, int toMine)
    {
        builder = null;

        if (builders.Count == 0)
        {
            return ObjectiveState.SuicideTroop;
        }
        else if (MiningBuilders() > 0)
        {
            if (toMine == -1)
                builder = GetMiningBuilder();
            else if (MiningBuilders()/2 >= MiningBuilders(toMine))
            {
                builder = GetMiningBuilder(toMine);
            }
            return ObjectiveState.Activated;
        }
        else
        {
            return ObjectiveState.NeedWait;
        }
    }

    BuilderUnit GetInConstructionBuilder()
    {
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsBuilding())
            {
                return builder;
            }
        }
        return null;
    }

    float InConstructionBuilders()
    {
        float res = 0;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsBuilding())
                res++;
        }
        return res;
    }

    BuilderUnit GetMiningBuilder()
    {
        int ore = MiningBuilders(1);
        int food = MiningBuilders(2);
        int index = (ore < food) ? 2 : 1;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsMining() && builder.GetComponent<MiningSystem>().currentResourceUnit.GetResourceIndex() == index)
            {
                return builder;
            }
        }
        return null;
    }

    BuilderUnit GetMiningBuilder(int index)
    {
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsMining() && builder.GetComponent<MiningSystem>().currentResourceUnit.GetResourceIndex() != index)
            {
                return builder;
            }
        }
        return null;
    }

    int MiningBuilders()
    {
        int res = 0;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsMining())
                res++;
        }
        return res;
    }

    int MiningBuilders(int resourceIndex)
    {
        int res = 0;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsMining() && builder.GetComponent<MiningSystem>().currentResourceUnit.GetResourceIndex() == resourceIndex)
                res++;
        }
        return res;
    }

    BuilderUnit GetJoblessBuilder()
    {
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsDoingNothing())
            {
                return builder;
            }
        }
        return null;
    }

    int JoblessBuilders()
    {
        int res = 0;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsDoingNothing())
                res++;
        }
        return res;
    }

    public void DivideMiner()
    {
        int ore = MiningBuilders(1);
        int food = MiningBuilders(2);
        int index = (ore < food) ? 2 : 1;
        int index2 = (ore < food) ? 1 : 2;
        while (Mathf.Abs(ore - food) > 1)
        {
            foreach (BuilderUnit builder in builders)
            {
                if (builder.IsMining() && builder.GetComponent<MiningSystem>().currentResourceUnit.GetResourceIndex() == index)
                {
                    GetComponent<BotMiningManager>().SendToMine(builder, index2);
                    if (ore < food)
                    {
                        ore++;
                        food--;
                    }
                    else
                    {
                        ore--;
                        food++;
                    }
                    break;
                }
            }
        }
    }
}
