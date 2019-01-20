using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerMenu : MonoBehaviour {

    [SerializeField]
    private GameObject joinGameMenu;

    public void JoinGameMenu()
    {
        joinGameMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
