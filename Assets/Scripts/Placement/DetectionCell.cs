using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCell : MonoBehaviour {

    public float localCellSize;

    [SerializeField]
    private List<RayCaster> rayCasters;
    private Material material;

    private Color green = new Color(0, 0.5f, 0, 0.4f);
    private Color red = new Color(0.5f, 0, 0, 0.4f);

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        material.color = Color.green;
        transform.localScale = new Vector3(localCellSize, localCellSize, localCellSize);
    }

    //
    public bool CheckAvailability()
    {
        bool available = true;

        foreach (RayCaster rc in rayCasters)
        {
            if (!rc.IsAvailable())
                available = false;
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
