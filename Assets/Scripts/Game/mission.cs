using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mission : MonoBehaviour
{
    [SerializeField] public GameObject independentBotPrefab;

    private bool isFirstTick = true;
    private int waveTime = 10;
    private int waveCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        independentBotPrefab.GetComponent<IndependantIAManager>()
            .InstantiateUnit("Units/Basic Troop", new Vector3(0,0,10), Quaternion.Euler(0, 0, 0));        
    }

    
    void Update()
    {
        int timer = (int)InstanceManager.instanceManager.timer;
        if (isFirstTick && timer == waveTime)
        {
            isFirstTick = false;
            Wave();
            independentBotPrefab.GetComponent<BotArmyManager>().SendArmy(new Vector3(0,0,10));
            Debug.Log(waveCount);
        }

        if (timer == waveTime+1)
        {
            isFirstTick = true;
            
            waveTime += 30;
            Debug.Log(waveTime);
            
        }
    }

    void Wave()
    {
        for (int i = 0; i < waveCount; i++)
        {
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(30,0,30), Quaternion.Euler(0, 0, 0));   
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(30,0,-30), Quaternion.Euler(0, 0, 0));   
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-30,0,30), Quaternion.Euler(0, 0, 0));   
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-30,0,-30), Quaternion.Euler(0, 0, 0));   
        }

        waveCount += 1;
    }

    /*void Wave(int UnitSpawned)
    {
       switch (pos)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4: break;               
        }
        Spawnunit(UnitSpawned,new Vector3(-500,-382,10));;
    }

    void Spawnunit(int UnitSpawned, Vector3 coords)
    {
        for (int i = 0; i < UnitSpawned; i++)
        {            
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", coords, Quaternion.Euler(0, 0, 0));
            
        }
    }*/
}
