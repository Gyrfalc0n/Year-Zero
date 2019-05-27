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
        if (isFirstTick)
        {
            isFirstTick = false;
            Wave(wave);            
            if(wave == 1){}
                
            if (wave == 2)
            {
                
            }
            if(wave == 3){}       
            
        }

        if (InstanceManager.instanceManager.allSelectableObjs.Count - InstanceManager.instanceManager.mySelectableObjs.Count == 0)
        {
            wave++;
            isFirstTick = true;            
        }
        

    }

    void Wave(int n)
    {
        if (n == 1)
        {
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Builder", new Vector3(-25,0,-25), Quaternion.Euler(0, 0, 0)); //1
        }
        if (n == 2)
        {
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Builder", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)); //2
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        }
        if (n == 3)
        {
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(30, 0, 30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(30, 0, 30), Quaternion.Euler(0, 0, 0)); //3
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Basic Troop", new Vector3(30, 0, 30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Builder", new Vector3(30, 0, 30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Builder", new Vector3(30, 0, 30), Quaternion.Euler(0, 0, 0));
            independentBotPrefab.GetComponent<IndependantIAManager>()
                .InstantiateUnit("Units/Builder", new Vector3(30, 0, 30), Quaternion.Euler(0, 0, 0));
        }
    }

}
