using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PseudoInputField : MonoBehaviour {

    const string playerNamePrefKey = "PlayerName";

    [SerializeField]
    InputField inputField;

    public void CheckPseudo()
    {
        inputField.text = PlayerPrefs.GetString(playerNamePrefKey);
    }

    public void SetPseudo(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}


