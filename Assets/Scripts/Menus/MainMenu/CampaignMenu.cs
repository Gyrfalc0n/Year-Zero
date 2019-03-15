using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class CampaignMenu : MonoBehaviourPunCallbacks
 {
    
     public void StartTuto()
     {
        //SceneManager.LoadScene(4); 
        PhotonNetwork.CreateRoom("Tutoz", new Photon.Realtime.RoomOptions { MaxPlayers = 1 });
     }
 
     public void StartMission()
     {
         if ((PlayerPrefs.GetInt("tutoCleared",0)==1))
         {
             PhotonNetwork.LoadLevel(5);
                         
         }
     }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        PhotonNetwork.LoadLevel(4);
    }
}