using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLifeBar : MonoBehaviour
{
    [SerializeField]
    Slider obj;
    DestructibleUnit associatedUnit;

    Vector3 tmp;

    public void Init(DestructibleUnit unit)
    {
        associatedUnit = unit;
    }

    void Update()
    {
        if (associatedUnit == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (associatedUnit.selected || associatedUnit.highlighted)
            {
                obj.gameObject.SetActive(true);
                tmp = associatedUnit.transform.position;
                tmp.y += 1;
                transform.position = tmp;
                transform.rotation = Camera.main.transform.rotation;

                obj.value = associatedUnit.GetLife() / associatedUnit.GetMaxlife() * obj.maxValue;
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
