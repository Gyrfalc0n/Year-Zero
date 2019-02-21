using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilltreePanel : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    public void ShowPanel()
    {
        obj.SetActive(true);
    }

    public void Cancel()
    {
        obj.SetActive(false);
    }
}
