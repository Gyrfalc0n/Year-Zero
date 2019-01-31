using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class AlliesPanel : MonoBehaviour
{
    AlliesMenu menu;
    Player associatedPlayer;

    int addAmount = 100;

    [SerializeField]
    Text playerName;
    [SerializeField]
    Text energy;
    [SerializeField]
    Text ore;
    [SerializeField]
    Text food;

    public void Init(Player player, AlliesMenu alliesMenu)
    {
        menu = alliesMenu;
        associatedPlayer = player;
        playerName.text = player.NickName;
    }

    void AddResource(int index, Text txt)
    {
        int myVal = PlayerManager.playerManager.Get(index);

        int tmp = int.Parse(txt.text);
        if (tmp + addAmount <= myVal)
        {
            txt.text = (tmp + addAmount).ToString();
        }
        else
        {
            txt.text = "0";
        }
    }

    public void AddEnergy()
    {
        AddResource(0, energy);
    }

    public void AddOre()
    {
        AddResource(1, ore);
    }

    public void AddFood()
    {
        AddResource(2, food);
    }

    public void Send()
    {
        int i0 = int.Parse(energy.text);
        int i1 = int.Parse(ore.text);
        int i2 = int.Parse(food.text);
        menu.SendResources(associatedPlayer, new List<int>() { i0, i1, i2 });
    }
}
