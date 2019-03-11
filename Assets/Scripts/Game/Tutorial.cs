using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private int step = 0;
    public Text tutoText;

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Event();
        }
    }

    void Event()
    {
        switch (step)
        {
            case 0:
                tutoText.text = "We will present you things";
                break;
            case 1:
                tutoText.text = "and others";
                break;
        }

        step++;
    }
}
