using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapLayersToggle : MonoBehaviour
{
    bool groundLayer = true;
    bool interactableLayer = true;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void UpdateLayer()
    {
        if (groundLayer && interactableLayer)
        {
            cam.cullingMask = (1 << LayerMask.NameToLayer("GroundLayer")) | (1 << LayerMask.NameToLayer("Minimap Icon"));
        }
        else if (groundLayer)
        {
            cam.cullingMask = (1 << LayerMask.NameToLayer("GroundLayer"));
        }
        else if (interactableLayer)
        {
            cam.cullingMask = (1 << LayerMask.NameToLayer("Minimap Icon"));
        }
        else
        {
            cam.cullingMask = 0;
        }
    }

    public void ToggleGround()
    {
        groundLayer = !groundLayer;
        UpdateLayer();
    }

    public void ToggleInteractable()
    {
        interactableLayer = !interactableLayer;
        UpdateLayer();
    }
}
