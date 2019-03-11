using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private bool w8 = false;
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
        if (w8 && InstanceManager.instanceManager.mySelectableObjs.Count == 2)
        {
            Time.timeScale = 0;
            TutoPause.SetActive(true);
            step++;
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
                tutoText.text = "Now Select the SpaceStation and click on the worker button to create one.";
                w8 = true;
                TutoPause.SetActive(false);
                Time.timeScale = 1;
                w8 = false;
                break;
            case 4 :
                tutoText.text ="Every unit cost a certain amount of resources, and take a certain time to be produced.";
                step++;
                break;
            case 5:
                tutoText.text = "Good management of these resources is the key (for/to?) victory";
                step++;
                break;
            case 6:
                tutoText.text = "Now select the worker";
                step++;
                break;
                
        }           
    }
}
