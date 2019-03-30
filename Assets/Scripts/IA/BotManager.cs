using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    GameResource[] resources = new GameResource[] { new GameResource("Energy")
,new GameResource("Ore")
,new GameResource("Food")
    ,new GameResource("Tech")};

    Population population = new Population();

    List<TownHall> homes = new List<TownHall>();

    public bool Pay(int[] costs, int pop)
    {
        if (PayCheck(costs, pop))
        {
            int i = 0;
            foreach (GameResource resource in resources)
            {
                resource.Remove(costs[i]);
                i++;
            }
            population.Add(pop);
            return true;
        }
        print("wtf");
        return false;
    }

    public bool PayCheck(int[] costs, int pop)
    {
        int i = 0;
        bool possible = true;
        foreach (GameResource resource in resources)
        {
            if (resource.GetValue() < costs[i])
                possible = false;
            i++;
        }
        if (possible && population.GetCurrentMaxValue() < population.GetValue() + pop)
        {
            possible = false;
        }
        return possible;
    }

    public int GetPayLimiterIndex(int[] costs, int pop)
    {
        int res = -1;
        if (!PayCheck(costs, pop))
        {
            for (int i = 0; i < costs.Length; i++)
            {
                if (resources[i].GetValue() < costs[i])
                    return i;
            }
            if (population.GetCurrentMaxValue() < population.GetValue() + pop)
                return -2;
        }
        return res;
    }

    public void Add(int val, int index)
    {
        resources[index].Add(val);
    }

    public int Get(int index)
    {
        return resources[index].GetValue();
    }

    public void Remove(int val, int index)
    {
        resources[index].Remove(val);
    }

    #region homes

    public void AddHome(TownHall home)
    {
        homes.Add(home);
    }

    public List<TownHall> GetHomes()
    {
        return homes;
    }

    public void RemoveHome(TownHall home)
    {
        homes.Remove(home);
    }

    #endregion

    #region population

    public void AddMaxPopulation(int val)
    {
        population.AddMax(val);
    }

    public int GetPopulation()
    {
        return population.GetValue();
    }

    public int GetMaxPopulation()
    {
        return population.GetCurrentMaxValue();
    }

    public void RemovePopulation(int val)
    {
        population.Remove(val);
    }

    #endregion
}
