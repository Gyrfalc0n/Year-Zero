using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionMenu : MonoBehaviour
{
    [SerializeField]
    Text dots;
    float timer;

    void Start()
    {
        timer = 0.3f;
    }

    void Update()
    {
        UpdateDots();
    }

    void UpdateDots()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.3f;
            if (dots.text.Length == 5)
                dots.text = "";
            else
                dots.text = new string('.', (dots.text.Length + 1));
        }
    }
}
