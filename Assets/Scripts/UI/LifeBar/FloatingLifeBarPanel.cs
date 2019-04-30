using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingLifeBarPanel : MonoBehaviour
{
    [SerializeField] FloatingLifeBar bar;

    public void Show(DestructibleUnit unit)
    {
        bar.gameObject.SetActive(true);
        bar.Set(unit);
    }

    public void Hide()
    {
        bar.gameObject.SetActive(false);
    }
}
