using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinGameMenu : MonoBehaviourPunCallbacks {

    private RoomInfo selectedRoom;

    [SerializeField]
    private Button joinGameButton;
    [SerializeField]
    private InputField gameNameInputField;

    private void Start()
    {
        joinGameButton.interactable = false;
    }

    public void OnValueChanged(string value)
    {
        if (gameNameInputField.text != null || gameNameInputField.text != string.Empty)
        {
            joinGameButton.interactable = true;
        }
        else
        {
            joinGameButton.interactable = false;
        }
    }

    public void JoinGame()
    {
        PhotonNetwork.JoinRoom(gameNameInputField.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room not found");
    }
}
