using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : ConstructedUnit
{
    MinimapMarkerControls minimap;
    MovementControls movementControls;
    TemporaryMessage message;

    List<MovableUnit> list = new List<MovableUnit>();

    void Start()
    {
        GameObject tmp = GameObject.Find("InstanceManager");
        minimap = tmp.GetComponent<MinimapMarkerControls>();
        movementControls = tmp.GetComponent<MovementControls>();
        message = GameObject.Find("Attacked Message").GetComponent<TemporaryMessage>();
    }
    void OnTriggerEnter(Collider other)
    {
        bool empty = list.Count == 0;
        if (other.GetComponent<MovableUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<MovableUnit>().photonView.Owner) &&
            !list.Contains(other.GetComponent<MovableUnit>()))
        {
            list.Add(other.GetComponent<MovableUnit>());
            if (empty)
            {
                minimap.CreateMarker(movementControls.WorldSpaceToMinimap(other.transform.position));
                message.Activate();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovableUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<MovableUnit>().photonView.Owner) &&
    list.Contains(other.GetComponent<MovableUnit>()))
        {
            list.Remove(other.GetComponent<MovableUnit>());
        }
    }

    public override bool IsAvailable()
    {
        int count = 0;
        int townHallLevel = 0;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Radar>() != null)
            {
                count++;
            }
            if (obj.GetComponent<InConstructionUnit>() != null)
            {
                if (obj.GetComponent<InConstructionUnit>().GetAssociatedBuilding().GetComponent<Radar>() != null)
                    count++;
            }
            if (obj.GetComponent<TownHall>() != null)
            {
                townHallLevel = Mathf.Max(townHallLevel, 1);
            }
        }
        return count < townHallLevel;
    }
}
