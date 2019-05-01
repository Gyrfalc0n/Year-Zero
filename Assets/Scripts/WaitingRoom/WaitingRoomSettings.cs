using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomSettings : MonoBehaviourPunCallbacks, IPunObservable
{
    public Dropdown raceDropdown;
    public Dropdown teamDropdown;
    public Dropdown colorDropdown;
    public GameObject readyText;
    public GameObject notReadyText;
    [SerializeField]
    protected GameObject kickButton;

    protected void Start()
    {
        InitPanel();
    }

    protected virtual void InitPanel()
    {
        Transform playersList = GameObject.Find("PlayersList").transform;
        transform.SetParent(playersList);
        transform.localScale = new Vector3(1, 1, 1);
        if (photonView.IsMine)
        {
            raceDropdown.interactable = true;
            teamDropdown.interactable = true;
            colorDropdown.interactable = true;
        }
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(raceDropdown.value);
            stream.SendNext(teamDropdown.value);
            stream.SendNext(colorDropdown.value);
        }
        else
        {
            raceDropdown.value = (int)stream.ReceiveNext();
            teamDropdown.value = (int)stream.ReceiveNext();
            colorDropdown.value = (int)stream.ReceiveNext();
        }
    }

    public virtual void Kick() { }

    public void Lock()
    {
        raceDropdown.interactable = false;
        teamDropdown.interactable = false;
        colorDropdown.interactable = false;
        readyText.SetActive(true);
        notReadyText.SetActive(false);
    }

    public void Unlock()
    {
        raceDropdown.interactable = true;
        teamDropdown.interactable = true;
        colorDropdown.interactable = true;
        readyText.SetActive(false);
        notReadyText.SetActive(true);
    }
}
