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

    public ObjectiveState GetOneBuilder(out BuilderUnit builder, bool forHouse, bool toMine)
    {
        builder = GetJoblessBuilder();
        if (builder != null)
            return ObjectiveState.Activated;

        if (builders.Count >= m.GetMaxPopulation() || builders.Count/m.GetMaxPopulation()*100 >= 25)
        {
            float inConstruction = InConstructionBuilders();
            if (inConstruction == builders.Count || inConstruction/builders.Count*10 > 35)
            {
                return ObjectiveState.NeedWait;
            }
            else
            {
                return TakeOrHouse(out builder, forHouse, toMine);
            }
        }
        else
        {
            if (m.GetPopulation() == m.GetMaxPopulation())
            {
                return TakeOrHouse(out builder, forHouse, toMine);
            }
            else
            {
                return ObjectiveState.NeedBuilder;
            }
        }
    }

    ObjectiveState TakeOrHouse(out BuilderUnit builder, bool forHouse, bool toMine)
    {
        if (GetComponent<BotConstructionManager>().GetHouseCount() < GetComponent<IAObjectivesManager>().step + 2 * GetComponent<IAObjectivesManager>().step)
        {
            if (forHouse || toMine)
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
            return TakeBuilder(out builder, forHouse, toMine);
        }
    }

    ObjectiveState TakeBuilder(out BuilderUnit builder, bool forHouse, bool toMine)
    {
        builder = null;

        if (builders.Count == 0)
        {
            return ObjectiveState.SuicideTroop;
        }
        else if (MiningBuilders() > 0)
        {
            if (toMine)
                return ObjectiveState.Done;
            builder = GetMiningBuilder();
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
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsMining())
            {
                return builder;
            }
        }
        return null;
    }

    float MiningBuilders()
    {
        float res = 0;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsMining())
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

    float JoblessBuilders()
    {
        float res = 0;
        foreach (BuilderUnit builder in builders)
        {
            if (builder.IsDoingNothing())
                res++;
        }
        return res;
    }
}
