using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotArmyManager : MonoBehaviour
{
    List<MovableUnit> army = new List<MovableUnit>();

    public void Add(MovableUnit unit)
    {
        if (unit.GetComponent<BuilderUnit>() == null)
            army.Add(unit);
    }
}
