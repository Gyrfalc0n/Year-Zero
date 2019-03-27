using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMiningManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SendToMine(GetComponent<IAManager>().GetJoblessBuilders(), 1);
        }
    }

    void SendToMine(List<BuilderUnit> builders, int index)
    {
        for (int i = 0; i < builders.Count; i++)
        {
            builders[i].Mine(GetNearestAvailableResourceUnit(builders[i], index));
        }
    }

    ResourceUnit GetNearestAvailableResourceUnit(BuilderUnit unit1, int index)
    {
        List<ResourceUnit> r = InstanceManager.instanceManager.allResourceUnits;
        int max = r.Count;

        ResourceUnit res = null;

        for (int i = 0; i < max; i++)
        {
            if ((res == null) || (Vector3.Distance(unit1.transform.position, r[i].transform.position) < Vector3.Distance(unit1.transform.position, res.transform.position)))
            {
                res = r[i];
            }
        }
        return res;
    }
}
