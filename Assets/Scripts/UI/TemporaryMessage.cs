using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryMessage : MonoBehaviour
{
    #region Singleton

    public static TemporaryMessage temporaryMessage;

    void Awake()
    {
        temporaryMessage = this;
    }

    #endregion

    [SerializeField]
    Transform content;
    [SerializeField]
    TemporaryMessagePrefab temporaryMessagePrefab;

    public void Add(string message)
    {
        Instantiate(temporaryMessagePrefab, content).Init(message);
    }
}
