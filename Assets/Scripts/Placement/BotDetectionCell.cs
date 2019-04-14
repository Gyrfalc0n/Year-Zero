using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDetectionCell : DetectionCell
{
    public override void Init()
    {
        transform.localScale = new Vector3(localCellSize, localCellSize, localCellSize);
    }

    public override bool CheckAvailability()
    {
        bool available = true;
        for (int i = 0; i < rayCasters.Count && available; i++)
        {
            if (!rayCasters[i].IsAvailable())
                available = false;
            i++;
        }
        return available;
    }
}
