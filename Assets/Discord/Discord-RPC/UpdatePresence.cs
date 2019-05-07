using System.Collections;
using System.Collections.Generic;
using DiscordPresence;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdatePresence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameTest")
        {
            if (PhotonNetwork.OfflineMode == true)
            {
                PresenceManager.UpdatePresence(detail: "Dans une partie en solo");
            }
            else
            {
                PresenceManager.UpdatePresence(detail: "Dans une partie en ligne");
            }

        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PresenceManager.UpdatePresence(detail: "Dans les menus");
        }
        else if (SceneManager.GetActiveScene().name == "WaitingRoom")
        {
            PresenceManager.UpdatePresence(detail: "Dans une salle d'attente");
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "Mission" ||
                 SceneManager.GetActiveScene().name == "Mission2")
        {
            PresenceManager.UpdatePresence(detail: "Dans la campagne solo");
        }
        else
        {
            PresenceManager.UpdatePresence(detail: "Dans le mode sans fin");
        }
    }
}
