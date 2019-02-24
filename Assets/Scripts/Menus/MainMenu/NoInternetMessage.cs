using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInternetMessage : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    float time;
    [SerializeField]
    float maxTime = 5f;

    public void Activate()
    {
        obj.SetActive(true);
        time = maxTime;
    }

    private void Update()
    {
        if (obj.activeInHierarchy)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                obj.SetActive(false);
            }
        }
    }
}
