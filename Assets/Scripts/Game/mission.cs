using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class mission : MonoBehaviour
{
    [SerializeField] public GameObject independentBotPrefab;
    [SerializeField] public Text Display;
    [SerializeField] public GameObject Background;
    public bool isNormal;

    private bool isFirstTick = true;
    private int waveTime = 10;
    private int waveCount = 0;
    private int[] numberPerWave = {0, 1, 2, 3, 5, 0, 0, 0};
    private string[] messageOfWave =
    {
        "Instructor: Create units to be prepared",
        "Instructor: Protect your base from enemies",
        "Instructor: Hold on!",
        "Instructor: More are coming at you",
        "Instructor: Last one you are almost done with them",
        "Instructor: Last one you are almost done with them",
        "Instructor: Last one you are almost done with them",
        "Instructor: Good Job!" + "\n" + "Endless mode and Mission 2 unlocked!"
    };
    private string[] voicesToPlay =
    {
        "VoixMission1.1",
        "VoixMission1.2",
        "VoixMission1.3",
        "NameNotMissing",
        "NameNotMissing",
        "NameNotMissing",
        "NameNotMissing",
        "VoixMission1.4"
    };
    
  

    
    void Update()
    {
        int timer = (int)InstanceManager.instanceManager.timer;
        if (isFirstTick && timer == waveTime)
        {
            isFirstTick = false;
            Wave();
            independentBotPrefab.GetComponent<BotArmyManager>().SendArmy(new Vector3(0,0,5));
            Background.SetActive(true);
            Display.text = messageOfWave[waveCount];
            FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[waveCount]);
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
            if (waveCount==1 && isNormal)
            {
                waveTime += 80;
            }
            waveTime += 20;
            waveCount += 1;
            if (waveCount > 7)
            {
                EndMission();
            }
            
        }
    }

    void EndMission()
    {
        PlayerPrefs.SetInt("missionCleared",1);
        PhotonNetwork.LoadLevel("MainMenu");
        FindObjectOfType<AudioManager>().PlaySound("MainMenuMusic");
    }
    void Wave()
    {
        for (int i = 0; i < numberPerWave[waveCount]; i++)
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
    }
}
