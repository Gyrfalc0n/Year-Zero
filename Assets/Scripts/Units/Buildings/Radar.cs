using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : ConstructedUnit
{
    MinimapMarkerControls minimap;
    MovementControls movementControls;

    List<MovableUnit> list = new List<MovableUnit>();

    void Start()
    {
        GameObject tmp = GameObject.Find("InstanceManager");
        minimap = tmp.GetComponent<MinimapMarkerControls>();
        movementControls = tmp.GetComponent<MovementControls>();
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
                TemporaryMessage.temporaryMessage.Add("We are attacked!");
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
}
