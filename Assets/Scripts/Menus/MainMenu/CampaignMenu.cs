using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignMenu : MonoBehaviour
 {
    
     public void StartTuto()
     {
         SceneManager.LoadScene(4);
         
     }
 
     public void StartMission()
     {
         if ((PlayerPrefs.GetInt("tutoCleared",0)==1))
         {
             SceneManager.LoadScene(5);
             
         }
     }
 }