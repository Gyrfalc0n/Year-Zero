using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilltreePanel : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    public void Show()
    {
        obj.SetActive(true);
    }

    public void Hide()
    {
        obj.SetActive(false);
    }

    public bool Activated()
    {
        return obj.activeInHierarchy;
    }
}
