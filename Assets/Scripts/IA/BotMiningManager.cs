using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMiningManager : MonoBehaviour
{
    public void SendToMine(BuilderUnit builder, int index)
    {
        builder.Mine(GetNearestAvailableResourceUnit(builder, index));
    }

    ResourceUnit GetNearestAvailableResourceUnit(BuilderUnit unit1, int index)
    {
        List<ResourceUnit> r = InstanceManager.instanceManager.allResourceUnits;
        int max = r.Count;

        ResourceUnit res = null;

        for (int i = 0; i < max; i++)
        {
            if (r[i].GetResourceIndex() == index && ((res == null) || (Vector3.Distance(unit1.transform.position, r[i].transform.position) < Vector3.Distance(unit1.transform.position, res.transform.position))))
            {
                res = r[i];
            }
        }
        return res;
    }
}
