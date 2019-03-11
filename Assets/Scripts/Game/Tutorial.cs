using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool waitForFirstWorker = false;
    private bool waitForSelection = false;
    private bool waitForCombatStation = false;
    private int step = 0;
    public Text tutoText;
    public bool runningTutorial = true;
    [SerializeField] private GameObject TutoPause;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject SpaceStationButton;


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
                /*if (selected.GetComponent<>()!=null)
                {
                    Time.timeScale = 0;
                    TutoPause.SetActive(true);
                    waitForSelection = false;
                    step++;
                }*/
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
                tutoText.text = "Click on build and try to build a Combat Station";
                waitForCombatStation = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                break;
            case 9:
                tutoText.text = "The Combat station allows you to create combat unit";
                step++;
                break;
            case 10:
                tutoText.text = "Select the Combat Station and create a basic troop ";
                //verification
                step++;
                break;
        }    
    }
}
