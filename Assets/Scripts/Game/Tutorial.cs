using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool waitForTreeSkill = false;
    private bool waitForFirstWorker = false;
    private bool waitForSelection = false;
    private bool waitForCombatStation = false;
    private bool waitForBasicTroop = false;
    private bool waitForMining = false;
    private int step = 0;
    public Text tutoText;
    public bool runningTutorial = true;
    [SerializeField] private GameObject TutoPause;
    [SerializeField] private GameObject Camera;
    [SerializeField] private SkilltreePanel TreeSkill;


    void Start()
    {
        Time.timeScale = 0;        
    }

    void Update()
    {       
        if (Input.GetMouseButtonDown(1))
        {
            TutoEvent();
        }
        if (waitForFirstWorker && InstanceManager.instanceManager.mySelectableObjs.Count == 2)
        {
            Time.timeScale = 0;
            TutoPause.SetActive(true);
            waitForFirstWorker = false;
            step++;
        }
        if (waitForSelection && SelectUnit.selectUnit.selected.Count != 0)
        {
            foreach (SelectableObj selected in SelectUnit.selectUnit.selected)
            {
                if (selected.GetComponent<BuilderUnit>()!=null)
                {
                    Time.timeScale = 0;
                    TutoPause.SetActive(true);
                    waitForSelection = false;
                    step++;
                }
            }    
        }

        if (waitForCombatStation)
        {
            foreach (SelectableObj selected in InstanceManager.instanceManager.mySelectableObjs)
            {
                if (selected.GetComponent<CombatStation>()!= null)
                {
                    Time.timeScale = 0;
                    TutoPause.SetActive(true);
                    waitForCombatStation = false;
                    step++;
                }
            }
        }
        if (waitForBasicTroop)
        {
            foreach (SelectableObj selected in InstanceManager.instanceManager.mySelectableObjs)
            {
                if (selected.GetComponent<BasicTroop>()!= null)
                {
                    Time.timeScale = 0;
                    TutoPause.SetActive(true);
                    waitForBasicTroop = false;
                    step++;
                }
            }
        }
        if (waitForTreeSkill && TreeSkill.Activated())
        {
            Time.timeScale = 0;
            TutoPause.SetActive(true);
            waitForTreeSkill = false;
            step++;
        }
        if (waitForMining)
        {
            foreach (SelectableObj selected in InstanceManager.instanceManager.mySelectableObjs)
            {
                if (selected.GetComponent<BuilderUnit>() != null)
                {
                    if (selected.GetComponent<BuilderUnit>().IsMining())
                    {
                        Time.timeScale = 0;
                        TutoPause.SetActive(true);
                        waitForMining = false;
                        step++; 
                    }
                }
            }
            
        }

        //  InstanceManager.instanceManager.mySelectableObjs  pour trouver la liste des objets selectionnables à nous
        //   SelectUnit.selectUnit.selected
        //   Time.timeScale = 0;      
    }


    void TutoEvent()
    {
        switch (step)
        {
            case 0:
                tutoText.text = "Year Zero is a Real Time Strategy game";
                step++;
                break;                
            case 1:
                Camera.GetComponent<CameraController>().LookTo(new Vector3(2f, 0.5f, 2f));
                tutoText.text = "This is a SpaceStation, it allows you to build workers.";
                step++;
                break;
            case 2:
                tutoText.text="If you loose all your SpaceStation, the game is over for you !";
                step++;
                break;
            case 3:
                tutoText.text = "Now Select the SpaceStation by drawing a square around it and click on the builder button to create one.";
                waitForFirstWorker = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 4 :
                tutoText.text ="Every unit cost a certain amount of resources, and take a certain time to be produced.";
                step++;
                break;
            case 5:
                tutoText.text = "Good management of these resources is the key to victory";
                step++;
                break;
            case 6:
                tutoText.text = "Now select the worker";
                waitForSelection = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 7:
                tutoText.text = "The worker can build building, and repair them";
                step++;
                break;
            case 8:
                tutoText.text = "Order him to harvest from the asteroid to gain ores";
                waitForMining = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 9:
                tutoText.text = "Click on build and try to build a Combat Station";
                waitForCombatStation = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 10:
                tutoText.text = "The Combat station allows you to create combat unit";
                step++;
                break;
            case 11:
                tutoText.text = "Select the Combat Station and create a basic troop ";
                waitForBasicTroop = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 12:
                tutoText.text = "Every building or unit works the same as builders";
                step++;
                break;
            case 13:
                tutoText.text = "In the right bottom area you can use its features";
                step++;
                break;
            case 14 :
                tutoText.text = "Another primary topic in Year Zero is the skill tree";
                step++;
                break;
            case 15:
                tutoText.text = "You can open it with the button in the top middle ";
                waitForTreeSkill = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 16:
                tutoText.text = "There you can choose from various upgrades";
                step++;
                break;
            case 17:
                tutoText.text = "but be careful, once one is selected its neighbours can't";
                step++;
                break;
            case 18:
                TreeSkill.Hide();
                tutoText.text = "The last thing is the map";
                step++;
                break;
            case 19:
                tutoText.text = "it allows you to see the whole game and to travel ";
                step++;
                break;
            case 20:
                tutoText.text = "Now you learned how to play Year Zero ";
                step++;
                break;
            case 21:
                tutoText.text = "Prepare to fight many epic battles!";
                PlayerPrefs.SetInt("tutoCleared",1);
                PhotonNetwork.LoadLevel("MainMenu");
                
                break;
            
        }    
    }
}
