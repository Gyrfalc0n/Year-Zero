using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager playerManager;

    public void Init()
    {
        playerManager = this;
    }

    #endregion

    [SerializeField]
    private ResourcesPanel resourcesPanel;

    GameResource[] resources = new GameResource[] { new GameResource("Energy")
,new GameResource("Ore")
,new GameResource("Food")
    ,new GameResource("Tech")};

    Population population = new Population();

    List<TownHall> homes = new List<TownHall>();

    public void UpdateResourcesPanel()
    {
        resourcesPanel.UpdatePanel();
    }

    public List<string> GetResourcesName()
    {
        List<string> names = new List<string>();
        foreach (GameResource gameResource in resources)
        {
            names.Add(gameResource.GetName());
        }
        return names;
    }

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
            UpdateResourcesPanel();
            return true;
        }
        return false;
    }

    public void PayBack(int[] costs, int pop)
    {
        int i = 0;
        foreach (GameResource resource in resources)
        {
            resource.Add(costs[i]);
            i++;
        }
        population.Remove(pop);
        UpdateResourcesPanel();
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
            
        if (!possible)
        {
            TemporaryMessage.temporaryMessage.Add("Not Enough Resources");
        }
        return possible;
    }

    public void Add(int val, int index)
    {
        resources[index].Add(val);
        UpdateResourcesPanel();
    }

    public int Get(int index)
    {
        return resources[index].GetValue();
    }

    public void Remove(int val, int index)
    {
        resources[index].Remove(val);
        UpdateResourcesPanel();
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
        UpdateResourcesPanel();
    }

    #endregion

    #region population

    public void AddMaxPopulation(int val)
    {
        population.AddMax(val);
        UpdateResourcesPanel();
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

    public void RemoveMaxPopulation(int val)
    {
        population.RemoveMax(val);
    }

    #endregion
}
