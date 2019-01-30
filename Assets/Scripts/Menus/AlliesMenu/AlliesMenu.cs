using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlliesMenu : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    //[SerializeField]
    //prefab

    public void ShowPanel()
    {
        obj.SetActive(true);
    }

    public void HidePanel()
    {
        obj.SetActive(false);
    }
}
