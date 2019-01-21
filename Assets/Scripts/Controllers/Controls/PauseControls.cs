using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseControls : PlayerControls
{
    [SerializeField]
    private GameObject menu;

    public void Init()
    {
        menu.SetActive(true);
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
        menu.SetActive(false);
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
