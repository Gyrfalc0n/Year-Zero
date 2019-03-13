using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turrel : ConstructedUnit
{
    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.turrel ;
    }
}
