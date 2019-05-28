using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager playerManager;

    void Awake()
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

    public bool Pay(int[] costs, int pop, bool dontcheckPop)
    {
        SkilltreeManager.manager.amountPaid += costs[3];
        if (InstanceManager.instanceManager.noCosts) return true;
        if (PayCheck(costs, pop, dontcheckPop))
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

    public bool PayCheck(int[] costs, int pop, bool dontcheckPop)
    {
        if (InstanceManager.instanceManager.noCosts) return true;
        int i = -1;
        bool possible = true;
        for (; possible && i+1 < resources.Length; i++)
        {
            if (resources[i+1].GetValue() < costs[i+1])
                possible = false;
        }
        if (!dontcheckPop && possible && population.GetCurrentMaxValue() < population.GetValue() + pop)
        {
            possible = false;
            i = -2;
        }

        if (!possible)
        {
            SendMessage(i);
        }
        return possible;
    }

    public void SendMessage(int i)
    {
        string message;
        switch (i)
        {
            case 0:
                message = "You don't have enough energy";
                FindObjectOfType<AudioManager>().PlaySound("NotEnoughEnergy");
                break;
            case 1:
                message = "You don't have enough ore";
                FindObjectOfType<AudioManager>().PlayRandomSound(new []{"NotEnoughOre","NotEnoughOre2"} );
                break;
            case 2:
                message = "You don't have enough food";
                FindObjectOfType<AudioManager>().PlaySound("NotEnoughFood");
                break;
            case 3:
                message = "You don't have enough tech points";
                break;
            default:
                message = "You don't have enough population";
                FindObjectOfType<AudioManager>().PlayRandomSound(new []{"NotEnoughPop1","NotEnoughPop2"} );
                break;
        }
        TemporaryMessage.temporaryMessage.Add(message);
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

    public TownHall GetNearestHome(Vector3 pos)
    {
        TownHall res = null;

        for (int i = 0; i < homes.Count; i++)
        {
            if (res == null || Vector3.Distance(pos, homes[i].transform.position) < Vector3.Distance(pos, res.transform.position))
            {
                res = homes[i];
            }
        }

        return res;
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
        UpdateResourcesPanel();
    }

    public void RemoveMaxPopulation(int val)
    {
        population.RemoveMax(val);
        UpdateResourcesPanel();
    }

    #endregion
}
