using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscordPresence;

public class StartRichTime : MonoBehaviour
{
    // Cette fonction permet de lancer un timer au moment où elle est lue.
    // Donc si cette fonction a été lue il y a deux minutes il y aura marqué que le joueur joue depuis 2 minutes.
    void Start()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int cur_time = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        PresenceManager.UpdatePresence(detail: "En chargement...", start: cur_time);
    }
}
