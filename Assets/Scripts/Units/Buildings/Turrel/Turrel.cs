using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turrel : ConstructedUnit
{
    public Transform firePoint;
    public TurretRotation turretRotation;

    public override void InitUnit(int botIndex)
    {
        fieldOfViewPrefabPath = "VFX/FogOfWar/TurrelFOV";
        base.InitUnit(botIndex);
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.turrel ;
    }
}
