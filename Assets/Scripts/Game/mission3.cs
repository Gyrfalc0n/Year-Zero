using System.Timers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class mission3 : MonoBehaviour
{
    [SerializeField] public GameObject independentBotPrefab;
    [SerializeField] public Text Display;
    [SerializeField] public GameObject Background;

    private bool isFirstTick = true;
    private string botPrefab = "IA/BotPrefab";
    private bool Sucess = false;
    private int finishtimer;
    

    


    private string[] voicesToPlay =
    {
        "VoixMission3.1",
        "VoixMission3.2",
            
    };     
    private void Start()
    {
        IAManager bot = Instantiate((GameObject)Resources.Load(botPrefab)).GetComponent<IAManager>();
        bot.gameObject.name = "Bot0";
        bot.Init(0, 1, 1, 1, new Vector3 (30, 1, -30));
    }
    void Update()
    {
        int timer = (int)InstanceManager.instanceManager.timer;
        if(timer == 0 && isFirstTick)
        {
            isFirstTick = false;
            FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[0]);            
        }
        if (InstanceManager.instanceManager.allSelectableObjs.Count -
            InstanceManager.instanceManager.mySelectableObjs.Count == 0 && !isFirstTick)
        {
            FindObjectOfType<AudioManager>().PlaySound(voicesToPlay[1]);
            Sucess = true;           
            finishtimer = timer;
            isFirstTick = true;
        }
        if (Sucess && timer == (finishtimer + 25))
        {
            EndMission();
        }
        
    }
    void EndMission()
    {
        PlayerPrefs.SetInt("mission3Cleared",1);
        PhotonNetwork.LoadLevel("MainMenu");
        FindObjectOfType<AudioManager>().PlaySound("MainMenuMusic");
    }   

 
}
