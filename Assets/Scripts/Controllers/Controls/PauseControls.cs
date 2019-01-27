﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseControls : PlayerControls
{
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject obj;

    public void Init()
    {
        obj.SetActive(true);
        mainMenu.SetActive(true);
        if (PhotonNetwork.OfflineMode)
        {
            Time.timeScale = 0;
        }
    }

    public override void RightClick()
    {
    }

    public override void Update()
    {
        if (active)
        {
            CheckMenu();
        }
    }

    public override void Cancel()
    {
        base.Cancel();
        obj.SetActive(false);
        if (PhotonNetwork.OfflineMode)
        {
            Time.timeScale = 1;
        }
    }

    void CheckMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }
}
