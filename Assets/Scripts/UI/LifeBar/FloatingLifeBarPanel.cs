using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingLifeBarPanel : MonoBehaviour
{
    [SerializeField]
    FloatingLifeBar lifeBarPrefab;

    public FloatingLifeBar AddLifeBar(DestructibleUnit unit)
    {
        FloatingLifeBar obj = Instantiate(lifeBarPrefab, transform);
        obj.Init(unit);
        return obj;
    }
}
