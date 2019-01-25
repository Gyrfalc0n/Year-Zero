using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerSettings : MonoBehaviourPunCallbacks, IPunObservable
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

    #region customProp

    readonly string race = "Race";
    readonly string team = "Team";
    readonly string color = "Color";

    #endregion

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            nameDropdown.interactable = true;
        }
    }

    void UpdateSlot(int val)
    {
        nameDropdown.value = val;
    }

    public void InitPanel()
    {
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        raceDropdown.interactable = true;
        teamDropdown.interactable = true;
        colorDropdown.interactable = true;
        nameDropdown.options[0].text = PhotonNetwork.LocalPlayer.NickName;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(raceDropdown.value);
            stream.SendNext(teamDropdown.value);
            stream.SendNext(colorDropdown.value);
            stream.SendNext(nameDropdown.value);
            stream.SendNext(nameDropdown.options[0].text);
        }
        else
        {
            raceDropdown.value = (int)stream.ReceiveNext();
            teamDropdown.value = (int)stream.ReceiveNext();
            colorDropdown.value = (int)stream.ReceiveNext();
            nameDropdown.value = (int)stream.ReceiveNext();
            nameDropdown.options[0].text = (string)stream.ReceiveNext();
            nameDropdown.RefreshShownValue();
        }
    }
}
