using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : ConstructedUnit
{
    MinimapMarkerControls minimap;
    MinimapController minimapController;

    List<MovableUnit> list = new List<MovableUnit>();

    public override void Init2()
    {
        base.Init2();
        if (botIndex == -1)
        {
            GameObject tmp = GameObject.Find("InstanceManager");
            minimap = tmp.GetComponent<MinimapMarkerControls>();
            minimapController = tmp.GetComponent<MinimapController>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        bool empty = list.Count == 0;
        if (other.GetComponent<MovableUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<MovableUnit>()) &&
            !list.Contains(other.GetComponent<MovableUnit>()))
        {
            list.Add(other.GetComponent<MovableUnit>());
            if (empty)
            {
                minimap.CreateMarker(minimapController.WorldSpaceToMinimap(other.transform.position));
                TemporaryMessage.temporaryMessage.Add("We are attacked!");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovableUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<MovableUnit>()) &&
    list.Contains(other.GetComponent<MovableUnit>()))
        {
            list.Remove(other.GetComponent<MovableUnit>());
        }
    }

    public override bool IsAvailable()
    {
        int amount = 0;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Radar>() != null)
            {
                amount++;
            }
        }
        return amount < SkilltreeManager.manager.radar;
    }
}
