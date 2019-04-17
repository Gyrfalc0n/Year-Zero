using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSoundtrack : MonoBehaviour
{

    
    [SerializeField] private GameObject MainMenuSoudtrack;
    void Awake() //quand une scene contenant MainMenuSoundtrack est créer, il est instancié verifie d'etre l'unique sinon se detruit pour en avoir un seul
        {
            GameObject[] obj= GameObject.FindGameObjectsWithTag("Music"); 
    
            if (obj.Length > 1)
            {
                Destroy(MainMenuSoudtrack);
            }
    
            DontDestroyOnLoad(MainMenuSoudtrack);
        }

    void Update() // vérifie qu'il n'est pas dans une scène ou il ne devrait pas être, note la musique ne gène pas pendant les chargements
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "MainMenu" && sceneName!= "WaitingRoom" && sceneName != "LoadingScene")
        {
           Destroy(MainMenuSoudtrack); 
        }
    }
}
