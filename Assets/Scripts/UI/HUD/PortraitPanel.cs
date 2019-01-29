using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{
    [SerializeField]
    Text lifeText;

    DestructibleUnit associatedUnit;

    [SerializeField]
    Camera cam;
    float modulo = 1;

    [SerializeField]
    GameObject obj;

    public void Init(DestructibleUnit unit)
    {
        obj.SetActive(true);
        associatedUnit = unit;
    }

    void Update()
    {
        if (associatedUnit != null)
        {
            lifeText.text = associatedUnit.GetLife() + "\\" + associatedUnit.GetMaxlife();
            Vector3 rot = associatedUnit.transform.localEulerAngles;
            Vector3 pos = associatedUnit.transform.localPosition;

            pos.y += 0.5f;
            pos.x += modulo * Mathf.Sin(rot.y * Mathf.Deg2Rad);
            pos.z += modulo * Mathf.Cos(rot.y * Mathf.Deg2Rad);
            cam.transform.localPosition = pos;

            rot.y += 180;
            cam.transform.localEulerAngles = rot;
        }
    }
}
