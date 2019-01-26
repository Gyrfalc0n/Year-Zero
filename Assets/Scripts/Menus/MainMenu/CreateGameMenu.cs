using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateGameMenu : MonoBehaviour {

    private string gameName;
    private byte maxPlayer = 4;
    private string mapName;

    [SerializeField]
    private string[] maps;

    [SerializeField]
    private GameObject mapButton;

    [SerializeField]
    private InputField gameNameText;
    [SerializeField]
    private Dropdown maxPlayerDropdown;
    [SerializeField]
    private Button createGameButton;
    [SerializeField]
    private GameObject selectionBox;
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    GameObject gameNameObj;
    [SerializeField]
    GameObject maxPlayerObj;

    void Awake()
    {
        selectionBox.SetActive(false);
        InitMaxPlayerDropdown();
        InitMapButtons();
        CheckCreateGameButton();
    }

    void OnEnable()
    {
        CheckOffline();
    }

    void CheckOffline()
    {
        if (PhotonNetwork.OfflineMode)
        {
            gameNameObj.SetActive(false);
            maxPlayerObj.SetActive(false);
        }
    }

    private void InitMapButtons()
    {
        foreach (string map in maps)
        {
            GameObject obj = Instantiate(mapButton, scrollViewContent);
            obj.GetComponentInChildren<Text>().text = map;
        }
    }

    private void InitMaxPlayerDropdown()
    {
        List<string> tmp = new List<string>();
        for (int i = 2; i < 5; i++)
        {
            tmp.Add((i).ToString());
        }
        maxPlayerDropdown.AddOptions(tmp);
        maxPlayerDropdown.value = maxPlayer - 2;
    }

    public void SetGameName(string value)
    {
        gameName = value;
        CheckCreateGameButton();
    }

    private void CheckCreateGameButton()
    {
        if (gameName == string.Empty || mapName == null || gameName == null)
        {
            createGameButton.interactable = false;
        }
        else
        {
            createGameButton.interactable = true;
        }

        if (PhotonNetwork.OfflineMode)
        {
            createGameButton.interactable = true;
        }
    }

    public void SetMaxPlayer(int value)
    {
        maxPlayer = (byte)(value + 2);
    }

    public void CreateGame()
    {
        PlayerPrefs.SetString("MapName", mapName);
        PhotonNetwork.CreateRoom(gameName, new RoomOptions { MaxPlayers = maxPlayer });
    }

    public void SelectMap(Transform button)
    {
        mapName = button.GetComponentInChildren<Text>().text;
        selectionBox.SetActive(true);
        selectionBox.transform.position = new Vector3(selectionBox.transform.position.x, button.position.y, selectionBox.transform.position.z);
        CheckCreateGameButton();
    }
}
