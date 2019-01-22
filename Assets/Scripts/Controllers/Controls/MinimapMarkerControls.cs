using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarkerControls : PlayerControls
{
    [SerializeField]
    Minimap minimap;

    public override void Update()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                LeftClick();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightClick();
            }
        }
    }

    public override void LeftClick()
    {
        //put balise
        Cancel();
    }
}
