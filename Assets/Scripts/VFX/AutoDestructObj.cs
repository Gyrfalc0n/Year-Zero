using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructObj : MonoBehaviour
{
    [SerializeField] int lifetime;
    float remainingTime;

    float fadeOutTime = 2f;
    float remainingFadeOutTime;
    float maxAlpha;

    [SerializeField] Transform holder;

    void Start()
    {
        if (holder == null) holder = transform;
        maxAlpha = 1;
        remainingFadeOutTime = fadeOutTime;
        remainingTime = lifetime;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            remainingFadeOutTime -= Time.deltaTime;
            ChangeAlpha(remainingFadeOutTime / fadeOutTime * maxAlpha);
        }
        if (remainingFadeOutTime <= 0)
        {
            Destroy(gameObject);
        }

    }

    void ChangeAlpha(float val)
    {
        foreach (Transform child in holder)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                Color tmp = child.GetComponent<MeshRenderer>().material.color;
                tmp.a = val;
                child.GetComponent<MeshRenderer>().material.color = tmp;
            }
        }
    }
}
