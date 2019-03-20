using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : ConstructedUnit
{
    Transform banner;
    Transform spawnPoint;

    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        Init();
    }

    public void Init()
    {
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
        if (banner == null)
            Init();
        if (photonView.IsMine)
            banner.gameObject.SetActive(true);
    }

    public override void Deselect()
    {
        base.Deselect();
        if (banner == null)
            Init();
        banner.gameObject.SetActive(false);
    }

    public bool CanProduct(MovableUnit unit)
    {
        bool res = false;

        for (int i = 0; i < tools.Count && !res; i++)
        {
            if (tools[i] == unit)
                res = true;
        }

        return res;
    }
}
