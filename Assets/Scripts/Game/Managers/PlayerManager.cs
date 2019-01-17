using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager playerManager;

    private void Awake()
    {
        playerManager = this;
    }

    #endregion

    [SerializeField]
    private ResourcesPanel resourcesPanel;

    GameResource[] resources = new GameResource[] { new GameResource("Metal")
,new GameResource("Stone")
,new GameResource("Gold")
,new GameResource("Meat")};

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

    public bool Pay(int[] costs)
    {
        if (PayCheck(costs))
        {
            int i = 0;
            foreach (GameResource resource in resources)
            {
                resource.Remove(costs[i]);
                i++;
            }
            UpdateResourcesPanel();
            return true;
        }
        return false;
    }

    public void PayBack(int[] costs)
    {
        int i = 0;
        foreach (GameResource resource in resources)
        {
            resource.Add(costs[i]);
            i++;
        }
    }

    public bool PayCheck(int[] costs)
    {
        int i = 0;
        bool possible = true;
        foreach (GameResource resource in resources)
        {
            if (resource.GetValue() < costs[i])
                possible = false;
            i++;
        }
        if (!possible)
            Debug.Log("Not enough resources");
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

    void Remove(int val, int index)
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

    public void AddPopulation(int val)
    {
        population.Add(val);
        UpdateResourcesPanel();
    }

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

    #endregion
}
