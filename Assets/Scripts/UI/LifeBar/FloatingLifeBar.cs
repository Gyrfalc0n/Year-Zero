using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingLifeBar : MonoBehaviour
{
    [SerializeField]
    Image obj;
    [SerializeField]
    GameObject bg;
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
            UpdateBar();
        }
        
    }

    void UpdateBar()
    {
        if (associatedUnit.highlighted && !associatedUnit.groupHighlight)
        {
            obj.gameObject.SetActive(true);
            bg.SetActive(true);
            tmp = associatedUnit.transform.position;
            tmp.y += 1;
            transform.position = tmp;
            transform.rotation = Camera.main.transform.rotation;

            if (!associatedUnit.photonView.IsMine)
            {
                print(associatedUnit.GetLife());
                print(associatedUnit.GetMaxlife());
            }
            obj.fillAmount = associatedUnit.GetLife() / associatedUnit.GetMaxlife();
        }
        else
        {
            obj.gameObject.SetActive(false);
            bg.SetActive(false);
        }
    }
}
