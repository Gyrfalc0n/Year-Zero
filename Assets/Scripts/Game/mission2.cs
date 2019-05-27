using UnityEngine;
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
        
    };     
    void Update()
    {
         int timer = (int)InstanceManager.instanceManager.timer;                                  
         if (timer == 10 && isFirstTick)
         {
             isFirstTick = false;
             Wave(wave);
             wave++;            
             independentBotPrefab.GetComponent<BotArmyManager>().SendArmyMission2(new Vector3(3,0,3));
             Wave(2);
             wave++;
         }

        if (timer == 20)
        {
            independentBotPrefab.GetComponent<BotArmyManager>().attackMission2();
        }

        if (InstanceManager.instanceManager.allSelectableObjs.Count -
            InstanceManager.instanceManager.mySelectableObjs.Count == 0 && wave == 3)
        {
            Wave(3);
            independentBotPrefab.GetComponent<BotArmyManager>().SendArmyMission2(new Vector3(-40,0,-40));
        }                                 
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
                .InstantiateUnit("Units/Basic Troop", new Vector3(0, 0, 1), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(1, 0, 0), Quaternion.Euler(0, 0, 0)); //2
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Light Troop", new Vector3(1, 0, -1), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Bomber", new Vector3(1, 0, 1), Quaternion.Euler(0, 0, 0));
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
        }
    }

}
