using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : ConstructedUnit
{
    Transform banner;
    Transform spawnPoint;

    public override void Awake()
    {
        base.Awake();
        banner = transform.Find("Banner");
        spawnPoint = transform.Find("SpawnPoint");
        banner.gameObject.SetActive(false);
    }

    public Vector3 GetBannerCoords()
    {
        return banner.position;
    }

    public Vector3 GetSpawnPointCoords()
    {
        return spawnPoint.position;
    }

    public void MoveBanner(Vector3 vec)
    {
        banner.position = vec;
    }

    public override void Select()
    {
        base.Select();
        banner.gameObject.SetActive(true);
    }

    public override void Deselect()
    {
        base.Deselect();
        banner.gameObject.SetActive(false);
    }
}
