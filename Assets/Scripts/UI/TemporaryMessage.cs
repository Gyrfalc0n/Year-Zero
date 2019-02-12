using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryMessage : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    float time;
    [SerializeField]
    float maxTime;

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
