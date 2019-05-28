using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [HideInInspector]
    public SelectableObj associatedUnit;
    [SerializeField]
    protected float requiredTime;
    protected float requiredTimeBonus = 1;
    protected float remainingTime = 0;
    protected float remainingTimeSpeedBonus = 1;
    [SerializeField]
    protected string errorMessage;
    [HideInInspector]
    public bool needSpellControls = true;

    public virtual void Start()
    {
        remainingTime = 0;
    }

    public virtual void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime * remainingTimeSpeedBonus;
        }
    }

    public void Use()
    {
        if (remainingTime <= 0)
        {
            remainingTime = requiredTime * requiredTimeBonus;
            Effect();
        }
    }

    public abstract void Effect();

    public float GetRequiredTime()
    {
        return requiredTime;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public bool IsAvailable()
    {
        return IsUnlocked() && remainingTime <= 0;
    }

    public virtual bool IsUnlocked()
    {
        return true;
    }

    public void SendError()
    {
        TemporaryMessage.temporaryMessage.Add(errorMessage);
    }

    [SerializeField] Sprite sprite;
    public Sprite GetSprite()
    {
        return sprite;
    }
}
