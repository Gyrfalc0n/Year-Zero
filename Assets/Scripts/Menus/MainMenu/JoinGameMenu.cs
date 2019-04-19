using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinGameMenu : MonoBehaviourPunCallbacks {

    private RoomInfo selectedRoom;

    [SerializeField]
    Button joinGameButton;
    [SerializeField]
    InputField gameNameInputField;
    [SerializeField]
    TemporaryMenuMessage noName;
    [SerializeField]
    TemporaryMenuMessage noRoom;

    void Start()
    {
        gameNameInputField.ActivateInputField();
    }

    private void Update()
    {
        if (!gameNameInputField.isFocused)
            gameNameInputField.ActivateInputField();

        if (Input.GetKeyUp(KeyCode.Return))
        {
            TryJoinGame();
        }
    }

    public void TryJoinGame()
    {
        if (gameNameInputField.text != null && gameNameInputField.text != string.Empty)
        {
            JoinGame();
        }
        else
        {
            noName.Activate();
        }
    }

    public void JoinGame()
    {
        PhotonNetwork.JoinRoom(gameNameInputField.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        noRoom.Activate();
    }
}
