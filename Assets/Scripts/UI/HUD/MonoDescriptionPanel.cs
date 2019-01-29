using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    GameObject obj;

    public void Init(DestructibleUnit unit)
    {
        obj.SetActive(true);
        nameText.text = unit.objName;
    }

    public void Reset()
    {
        obj.SetActive(false);
    }
}
