using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour {

    private PlayerManager manager;

    List<ResourceText> resourceTexts = new List<ResourceText>();

    [SerializeField]
    ResourceText resourceTextPrefab;

    [SerializeField]
    private Text population;

    private void Start()
    {
        manager = PlayerManager.playerManager;
        CreatePanel();
        UpdatePanel();
    }

    void CreatePanel()
    {
        List<string> tmp = manager.GetResourcesName();
        for (int i = tmp.Count - 1; i >= 0; i--)
        {
            ResourceText obj = Instantiate(resourceTextPrefab, transform);
            obj.SetName(tmp[i]);
            resourceTexts.Add(obj);
        }
    }

    public void UpdatePanel()
    {
        int i = resourceTexts.Count - 1;
        foreach (ResourceText text in resourceTexts)
        {
            text.SetValue(manager.Get(i));
            i--;
        }

        population.text = manager.GetPopulation().ToString() + "/" + manager.GetMaxPopulation().ToString();
    }
}
