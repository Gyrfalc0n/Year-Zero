using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerSettings : MonoBehaviour
{
    Hashtable customProp;
    [SerializeField]
    Dropdown nameDropdown;
    [SerializeField]
    Dropdown raceDropdown;
    [SerializeField]
    Dropdown teamDropdown;
    [SerializeField]
    Dropdown colorDropdown;

    Player associatedPlayer;

    #region customProp

    string race = "Race";
    string team = "Team";
    string color = "Color";

    #endregion

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            nameDropdown.interactable = true;
        }
    }

    public void OnValueChanged()
    {
        if (PhotonNetwork.LocalPlayer == associatedPlayer)
            UpdateProps();
    }

    public void InitPanel(Player player)
    {
        associatedPlayer = player;
        raceDropdown.interactable = true;
        teamDropdown.interactable = true;
        colorDropdown.interactable = true;
        nameDropdown.options[0].text = player.NickName;
        nameDropdown.RefreshShownValue();
    }

    public void UpdatePanel(Player player)
    {
        Hashtable tmp = player.CustomProperties;
        nameDropdown.options[0].text = player.NickName;
        nameDropdown.RefreshShownValue();
        nameDropdown.interactable = false;
        raceDropdown.value = (int)tmp[race];
        teamDropdown.value = (int)tmp[team];
        colorDropdown.value = (int)tmp[color];
    }

    void UpdateProps()
    {
        customProp = PhotonNetwork.LocalPlayer.CustomProperties;
        ChangeProp(race, raceDropdown.value);
        ChangeProp(team, teamDropdown.value);
        ChangeProp(color, colorDropdown.value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
    }

    void ChangeProp(string prop, int value)
    {
        if (customProp.ContainsKey(prop))
        {
            customProp[prop] = value;
        }
        else
        {
            customProp.Add(prop, value);
        }
    }

    public bool IsOpen()
    {
        return nameDropdown.value == 0;
    }
}
