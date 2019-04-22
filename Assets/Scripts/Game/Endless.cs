using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Endless : MonoBehaviour
{
    [SerializeField] public GameObject independentBotPrefab;
    [SerializeField] public Text Display;
    [SerializeField] public GameObject Background;

    private bool isFirstTick = true;
    private int waveTime = 60;
    private int waveCount = 0;
    private int ToSpawn = 0;
    
  

    
    void Update()
    {
        if (ToSpawn > 0)
        {
            Spawn();
            ToSpawn--;
        }
        int timer = (int)InstanceManager.instanceManager.timer;
        if (isFirstTick && timer == waveTime)
        {
            isFirstTick = false;
            ToSpawn += waveCount - 1;
            Background.SetActive(true);
            Display.text = "Waves: " + (waveCount );
        }

        if (timer == waveTime+1)
        {
            isFirstTick = true;
            
            
            
        }
        
        if (isFirstTick && timer == waveTime+5)
        {
            isFirstTick = false;
            Display.text = "";
            Background.SetActive(false);
        }

        if (timer == waveTime+6)
        {
            isFirstTick = true;
            waveTime += 30;
            waveCount += 1;
        }
    }
    
    void Spawn()
    {
        independentBotPrefab.GetComponent<IndependantIAManager>()
            .InstantiateUnit("Units/Basic Troop", new Vector3(30-ToSpawn,0,30-ToSpawn), Quaternion.Euler(0, 0, 0));   
        independentBotPrefab.GetComponent<IndependantIAManager>()
            .InstantiateUnit("Units/Basic Troop", new Vector3(30-ToSpawn,0,-30+ToSpawn), Quaternion.Euler(0, 0, 0));   
        independentBotPrefab.GetComponent<IndependantIAManager>()
            .InstantiateUnit("Units/Basic Troop", new Vector3(-30+ToSpawn,0,30-ToSpawn), Quaternion.Euler(0, 0, 0));   
        independentBotPrefab.GetComponent<IndependantIAManager>()
            .InstantiateUnit("Units/Basic Troop", new Vector3(-30+ToSpawn,0,-30+ToSpawn), Quaternion.Euler(0, 0, 0));  
        
        independentBotPrefab.GetComponent<BotArmyManager>().SendArmy(new Vector3(0,0,5));
    }
}
