using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialLoadingBar : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Set(float val)
    {
        image.fillAmount = val;
    }
}
