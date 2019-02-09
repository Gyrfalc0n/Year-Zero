using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoading : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
