using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapLayersToggle : MonoBehaviour
{ 
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void ToggleGround()
    {
        cam.clearFlags = (cam.clearFlags == CameraClearFlags.Skybox) ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;
    }

    public void ToggleInteractable()
    {
        cam.cullingMask = (cam.cullingMask == 0) ? (1 << LayerMask.NameToLayer("Minimap Icon")) : 0;
    }
}
