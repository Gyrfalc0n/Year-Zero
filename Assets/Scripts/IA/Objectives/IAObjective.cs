using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAObjective : MonoBehaviour
{
    protected bool activated = false;

    protected int returnValue;

    public virtual void Activate()
    {
        returnValue = -1;
        activated = true;
    }

    public bool IsActivated()
    {
        return activated;
    }

    public int GetResult()
    {
        return returnValue;
    }
}
