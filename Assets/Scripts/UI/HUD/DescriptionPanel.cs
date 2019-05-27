using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionPanel : MonoBehaviour {

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    Text objName;

    [SerializeField]
    Transform costPanel;

    [SerializeField]
    ResourceTextDescrPanel[] costs = new ResourceTextDescrPanel[3];
    [SerializeField]
    ResourceTextDescrPanel pop;

    [SerializeField]
    private Text description;

    #region Singleton

    public static DescriptionPanel m;
    private void Awake()
    {
        m = this;
    }

    #endregion

    public void Init(SelectableObj obj)
    {
        panel.SetActive(true);
        objName.text = obj.objName;

        for (int i = costs.Length - 1; i >= 0; i--)
        {
            costs[i].resourceName.text = PlayerManager.playerManager.GetResourcesName()[i] + " :";
            costs[i].value.text = (obj.costs[i]).ToString();
        }
        pop.gameObject.SetActive(true);
        pop.value.text = obj.GetComponent<SelectableObj>().pop.ToString();

        description.text = obj.description;
    }

    public void ResetPanel()
    {
        pop.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
