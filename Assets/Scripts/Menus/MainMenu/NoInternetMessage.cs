using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInternetMessage : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    float time;
    float maxTime = 5;

    public void Activate()
    {
        obj.SetActive(true);
        time = maxTime;
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                obj.SetActive(false);
            }
        }
    }
}
