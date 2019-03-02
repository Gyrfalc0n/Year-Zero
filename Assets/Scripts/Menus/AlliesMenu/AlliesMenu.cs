using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AlliesMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    Transform content;

    [SerializeField]
    AlliesPanel alliesPanelPrefab;

    public void ShowPanel()
    {
        obj.SetActive(true);
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player == PhotonNetwork.LocalPlayer)
                continue;
            AlliesPanel panel = Instantiate(alliesPanelPrefab, content);
            panel.Init(player, this);
        }
    }

    public void Cancel()
    {
        obj.SetActive(false);
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void Accept()
    {
        foreach (Transform transform in content)
        {
            transform.GetComponent<AlliesPanel>().Send();
        }
        Cancel();
    }

    public void SendResources(Player player, List<int> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] != 0)
            {
                photonView.RPC("RPCSendResources", player, i, values[i]);
                PlayerManager.playerManager.Remove(values[i], i);
            }
                
        }
        
    }

    [PunRPC]
    public void RPCSendResources(int index, int value)
    {
        PlayerManager.playerManager.Add(value, index);
    }

    public bool Activated()
    {
        return obj.activeInHierarchy;
    }
}
