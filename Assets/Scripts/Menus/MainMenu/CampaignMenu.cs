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
}