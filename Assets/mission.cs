using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mission : MonoBehaviour
{
    [SerializeField] public GameObject independentBotPrefab;

    private int Unitspawned = 1;
    
    // Start is called before the first frame update
    void Start()
    {
                              
    }

    // Update is called once per frame
    void Update()
    {
        if (InstanceManager.instanceManager.timer > 10)
        {
            Debug.Log("dfq");
            Wave(Unitspawned);
            Unitspawned += 1;
        }       
    }

    void Wave(int UnitSpawned)
    {
       /* switch (pos)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4: break;               
        }*/
        Spawnunit(UnitSpawned,new Vector3(-500,-382,10));;
    }

    void Spawnunit(int UnitSpawned, Vector3 coords)
    {
        for (int i = 0; i < UnitSpawned; i++)
        {            
            independentBotPrefab.GetComponent<IndependantIAManager>().InstantiateUnit("Units/Basic Troop", coords, Quaternion.Euler(0, 0, 0));
            
        }
    }
}
