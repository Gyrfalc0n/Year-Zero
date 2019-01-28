using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour {

    [SerializeField]
    LayerMask layerMasks;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    private float distanceMax;
    [SerializeField]
    private float distanceMin;

    //Returns true if the ray hit a groundLayer object and returns the distance between this.gameObject and the ground
    public bool IsAvailable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMasks))
        {
            if ((int) Mathf.Pow(2, hit.collider.gameObject.layer) == (int) groundLayer)
            {
                float dist = (transform.position.y - hit.point.y);
                return (dist < distanceMax && dist > distanceMin);
            }
        }
        return false;
    }
}
