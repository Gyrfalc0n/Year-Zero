using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceText : MonoBehaviour
{
    [SerializeField]
    Text value;
    [SerializeField]
    Text text;

    public void SetValue(int value)
    {
        this.value.text = value.ToString();
    }

    public void SetName(string name)
    {
        text.text = name;
    }
}
