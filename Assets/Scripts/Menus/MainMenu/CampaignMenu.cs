﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class CampaignMenu : MonoBehaviour
 {
    
     public void StartTuto()
     {
         SceneManager.LoadSceneAsync(4);
        // PhotonNetwork.LoadLevel(4);
              
     }
 
     public void StartMission()
     {
         if ((PlayerPrefs.GetInt("tutoCleared",0)==1))
         {
             PhotonNetwork.LoadLevel(5);
                         
         }
     }
 }