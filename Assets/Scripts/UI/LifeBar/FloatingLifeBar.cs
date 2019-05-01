using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLifeBar : MonoBehaviour
{
    [SerializeField]
    Image obj;
    DestructibleUnit associatedUnit;

    public void Set(DestructibleUnit unit)
    {
        associatedUnit = unit;
    }

    void Update()
    {
        if (associatedUnit != null)
        {
            UpdateBar();
        }        
    }

    Vector3 tmp;
    void UpdateBar()
    {
        tmp = associatedUnit.transform.position;
        tmp.y += 2;
        transform.position = tmp;
        transform.rotation = Camera.main.transform.rotation;
        obj.fillAmount = associatedUnit.GetLife() / associatedUnit.GetMaxlife();
    }
}
