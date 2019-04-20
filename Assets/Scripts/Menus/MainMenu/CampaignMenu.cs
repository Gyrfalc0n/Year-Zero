using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class CampaignMenu : MonoBehaviourPunCallbacks
 {
    
     public void StartTuto()
     {
        PhotonNetwork.LoadLevel(4);
        FindObjectOfType<AudioManager>().PlaySound("UniverseMusic");
     }
 
     public void StartMission()
     {
         if ((PlayerPrefs.GetInt("tutoCleared",0)==1))
         {
             PhotonNetwork.LoadLevel(5);
             FindObjectOfType<AudioManager>().PlaySound("BattleMusic");
         }
     }
     
     public void StartMission2()
     {
         if ((PlayerPrefs.GetInt("missionCleared",0)==1))
         {
             PhotonNetwork.LoadLevel("Mission2");
             FindObjectOfType<AudioManager>().PlaySound("BattleMusic");
         }
     }
     
     public void StartEndlessMode()
     {
         if ((PlayerPrefs.GetInt("missionCleared",0)==1))
         {
             PhotonNetwork.LoadLevel("EndlessMode");
             FindObjectOfType<AudioManager>().PlaySound("BattleMusic");
         }
     }
}