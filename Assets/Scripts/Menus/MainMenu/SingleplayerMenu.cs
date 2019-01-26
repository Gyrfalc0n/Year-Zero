using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject createGameMenu;

    public void Campaign()
    {

    }

    public void QuickMatch()
    {
        createGameMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
