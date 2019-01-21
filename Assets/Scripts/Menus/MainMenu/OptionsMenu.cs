using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {

    [SerializeField]
    private GameObject pseudo;
    [SerializeField]
    private GameObject video;
    [SerializeField]
    private GameObject sound;

    public void ChangePseudoMenu()
    {
        pseudo.SetActive(true);
        pseudo.GetComponent<PseudoInputField>().CheckPseudo();
        gameObject.SetActive(false);
    }

    public void VideoMenu()
    {
        video.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SoundMenu()
    {
        sound.SetActive(true);
        gameObject.SetActive(false);
    }
}
