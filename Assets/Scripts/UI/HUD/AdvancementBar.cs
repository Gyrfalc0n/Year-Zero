using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancementBar : MonoBehaviour {

    private InConstructionUnit currentBuilding;

    [SerializeField]
    private Slider bar;
    [SerializeField]
    private GameObject obj;

    public void Cancel()
    {
        currentBuilding.GetComponent<InConstructionUnit>().Cancel();    
    }

    public void Init(InConstructionUnit building)
    {
        if (!MultiplayerTools.IsMine(building)) return;

        obj.SetActive(true);
        currentBuilding = building;
        bar.value = currentBuilding.GetCurrentActionAdvancement();
    }

    private void Update()
    {
        if (obj.activeInHierarchy)
        {
            if (currentBuilding != null)
            { 
                UpdateBar();
            }
            else
            {
                ResetBar();
            }
        }
    }

    private void UpdateBar()
    {
        bar.value = currentBuilding.GetCurrentActionAdvancement();
    }

    public void ResetBar()
    {
        obj.SetActive(false);
        currentBuilding = null;
    }
}
