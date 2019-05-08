using System.Collections;
using System.Collections.Generic;
using DiscordPresence;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdatePresence : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
        return;
#endif

        string message;
        switch (SceneManager.GetActiveScene().name)
        {
            case "GameTest":
                message = PhotonNetwork.OfflineMode ? "Dans une partie en solo": "Dans une partie en ligne";
                break;
            case "MainMenu":
                message = "Dans les menus";
                break;
            case "WaitingRoom":
                message = "Dans une salle d'attente";
                break;
            case "Tutorial":
            case "Mission":
            case "Mission2":
                message = "Dans la campagne solo";
                break;
            default:
                message = "Dans le mode sans fin";
                break;
        }
        PresenceManager.UpdatePresence(detail: message);
    }
}
