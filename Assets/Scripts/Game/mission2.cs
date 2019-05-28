using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class mission2 : MonoBehaviour
{
    [SerializeField] public GameObject independentBotPrefab;
    [SerializeField] public Text Display;
    [SerializeField] public GameObject Background;

    private bool isFirstTick = true;
    private int wave = 1;
  
    private string[] voicesToPlay =
    {
        "VoixMission2.1",
        "VoixMission2.2",
        "VoixMission2.3",
        "VoixMission2.4"
        
    };     
    void Update()
    {
         int timer = (int)InstanceManager.instanceManager.timer;
        if (timer == 1 && isFirstTick)
        {
            FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[0]);
            isFirstTick = false;
        }
        if (timer == 9 && !isFirstTick) isFirstTick = true;
        
         if (timer == 25 && isFirstTick)
         {             
             FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[1]);
             isFirstTick = false;
             Wave(wave);
             wave++;            
             independentBotPrefab.GetComponent<BotArmyManager>().SendArmyMission2(new Vector3(0,0,0));
             Wave(2);
             wave++;
         }

        if (timer == 65)
        {
            FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[2]);
            independentBotPrefab.GetComponent<BotArmyManager>().STOPRUSH();
            isFirstTick = true;
        }
        
        if ((InstanceManager.instanceManager.allSelectableObjs.Count -
            InstanceManager.instanceManager.mySelectableObjs.Count == 0 && wave == 3) || timer > 600 && isFirstTick)
        {
            FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[3]);
            Wave(3);
            independentBotPrefab.GetComponent<BotArmyManager>().SendArmyMission2(new Vector3(-40,0,-40));
            wave++;
            isFirstTick = false;
        }     
        if (InstanceManager.instanceManager.allSelectableObjs.Count -
            InstanceManager.instanceManager.mySelectableObjs.Count == 0 && wave == 4)
        {
            EndMission();
        }                      
    }

    void EndMission()
    {
        PlayerPrefs.SetInt("mission2Cleared",1);
        PhotonNetwork.LoadLevel("MainMenu");
        FindObjectOfType<AudioManager>().PlaySound("MainMenuMusic");
    }   
    void Wave(int n)
    {
        if (n == 1)
        {
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Hacker", new Vector3(-35, 0, -35), Quaternion.Euler(0, 0, 0)); //1
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Hacker", new Vector3(-34, 0, -35), Quaternion.Euler(0, 0, 0)); //1
        }
        if(n == 2)
        {            
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(0, 0, 2), Quaternion.Euler(0, 0, 0)); //2
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(2, 0, 2), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(2, 0, -2), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-2, 0, -2), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-2, 0, 2), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(0, 0, -2), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(2, 0, 0), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-2, 0, 0), Quaternion.Euler(0, 0, 0));
        }
        if (n == 3)
        {
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-40, 0, -30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-40, 0, -30), Quaternion.Euler(0, 0, 0)); //3
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Bomber", new Vector3(-40, 0, -30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Bomber", new Vector3(-40, 0, -30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Bomber", new Vector3(-40, 0, -30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Bomber", new Vector3(-40, 0, -30), Quaternion.Euler(0, 0, 0));
            
        }
    }
}
