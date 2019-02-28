using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSpellObj : MonoBehaviour
{
    float time;

    void Start()
    {
        time = 15f;   
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(this);
        }
    }
}
