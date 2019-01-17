using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildToolControls : PlayerControls
{
    [SerializeField]
    PlacementGrid placementGrid;
    PlacementGrid currentPlacementGrid;

    public void CreatePlacementGrid(ConstructedUnit building, BuilderUnit builder)
    {
        currentPlacementGrid = Instantiate(placementGrid).GetComponent<PlacementGrid>();
        currentPlacementGrid.Init(building, builder);
    }

    public override void Update()
    {
        base.Update();
        if (active && currentPlacementGrid == null)
        {
            active = false;
        }
    }

    public override void Cancel()
    {
        base.Cancel();
        if (currentPlacementGrid != null)
        {
            GameObject tmp = currentPlacementGrid.gameObject;
            currentPlacementGrid = null;
            Destroy(tmp);
        }
    }
}
