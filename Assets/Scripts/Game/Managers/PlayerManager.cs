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

    GameResource wood = new GameResource();
    GameResource stone = new GameResource();
    GameResource gold = new GameResource();
    GameResource meat = new GameResource();

    Population population = new Population();

    List<TownHall> homes = new List<TownHall>();

    public void UpdateResourcesPanel()
    {
        resourcesPanel.UpdatePanel();
    }

    public void AddWood(int val)
    {
        wood.Add(val);
        UpdateResourcesPanel();
    }

    public void AddStone(int val)
    {
        stone.Add(val);
        UpdateResourcesPanel();
    }

    public void AddGold(int val)
    {
        gold.Add(val);
        UpdateResourcesPanel();
    }

    public void AddMeat(int val)
    {
        meat.Add(val);
        UpdateResourcesPanel();
    }

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

    public void AddHome(TownHall home)
    {
        homes.Add(home);
    }

    public int GetWood()
    {
        return wood.GetValue();
    }

    public int GetStone()
    {
        return stone.GetValue();
    }

    public int GetGold()
    {
        return stone.GetValue();
    }

    public int GetMeat()
    {
        return meat.GetValue();
    }

    public int GetPopulation()
    {
        return population.GetValue();
    }

    public int GetMaxPopulation()
    {
        return population.GetCurrentMaxValue();
    }

    public List<TownHall> GetHomes()
    {
        return homes;
    }



    public void RemoveWood(int val)
    {
        wood.Remove(val);
        UpdateResourcesPanel();
    }

    public void RemoveStone(int val)
    {
        stone.Remove(val);
        UpdateResourcesPanel();
    }

    public void RemoveGold(int val)
    {
        gold.Remove(val);
        UpdateResourcesPanel();
    }

    public void RemoveMeat(int val)
    {
        meat.Remove(val);
        UpdateResourcesPanel();
    }

    public void RemovePopulation(int val)
    {
        population.Remove(val);
    }

    public void RemoveHome(TownHall home)
    {
        homes.Remove(home);
        UpdateResourcesPanel();
    }
}
