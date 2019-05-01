using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConquestStation : ProductionBuilding
{
    public override Vector3 GetSelectionCircleSize()
    {
        return new Vector3(3, 3, 3);
    }

    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
