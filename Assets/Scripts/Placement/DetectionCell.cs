using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCell : MonoBehaviour {

    public float localCellSize;

    [SerializeField]
    protected List<RayCaster> rayCasters;
    private Material material;

    Color green = new Color(0, 0.5f, 0, 0.4f);
    Color red = new Color(0.5f, 0, 0, 0.4f);

    public virtual void Init()
    {
        material = GetComponent<Renderer>().material;
        material.color = green;
        transform.localScale = new Vector3(localCellSize, localCellSize, localCellSize);
    }

    public virtual bool CheckAvailability()
    {
        bool available = true;

        for (int i = 0; i < rayCasters.Count && available; i++)
        {
            if (!rayCasters[i].IsAvailable())
                available = false;
            i++;
        }

        if (available)
        {
            if (material.color != green)
                material.color = green;
            available = true;
        }
        else if (material.color != red)
        {
            material.color = red;
        }

        return available;
    }
}
