using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryMessagePrefab : MonoBehaviour
{
    [SerializeField]
    Text message;

    float remainingTime = 5f;

    public void Init(string message)
    { 
        this.message.text = message;
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
