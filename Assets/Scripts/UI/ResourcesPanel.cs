using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour {

    private PlayerManager manager;

    [SerializeField]
    private Text wood;
    [SerializeField]
    private Text stone;
    [SerializeField]
    private Text gold;
    [SerializeField]
    private Text meat;
    [SerializeField]
    private Text population;

    private void Start()
    {
        manager = PlayerManager.playerManager;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        wood.text = manager.GetWood().ToString();
        stone.text = manager.GetStone().ToString();
        gold.text = manager.GetGold().ToString();
        meat.text = manager.GetMeat().ToString();

        population.text = manager.GetPopulation().ToString() + "/" + manager.GetMaxPopulation().ToString();
    }
}
