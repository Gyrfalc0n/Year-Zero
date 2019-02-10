using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenu : MonoBehaviour
{
    public Slider camMoveMouseSpeed;
    public Toggle camMoveMouse;
    public Slider camMoveKeySpeed;
    public Toggle helpBubble;

    const string camMoveMouseSpeedPref = "camMoveMouseSpeed";
    const string camMoveMousePref = "camMoveMouse";
    const string camMoveKeySpeedPref = "camMoveKeySpeed";
    const string helpBubblePref = "helpBubble";

    void OnEnable()
    {
        camMoveMouseSpeed.value = PlayerPrefs.GetFloat("camMoveMouseSpeed");
        camMoveMouse.isOn = PlayerPrefs.GetInt("camMoveMouse") == 1;
        camMoveKeySpeed.value = PlayerPrefs.GetFloat("camMoveKeySpeed");
        helpBubble.isOn = PlayerPrefs.GetInt("helpBubble") == 1;
    }

    public void SetCamMoveMouseSpeed(float val)
    {
        PlayerPrefs.SetFloat("camMoveMouseSpeed", val);
    }

    public void SetCamMoveMouse(bool val)
    {
        PlayerPrefs.SetInt("camMoveMouse", (val) ? 1 : 0);
    }

    public void SetCamMoveKeySpeed(float val)
    {
        PlayerPrefs.SetFloat("camMoveKeySpeed", val);
    }

    public void SetHelpBubble(bool val)
    {
        PlayerPrefs.SetInt("helpBubble", (val) ? 1 : 0);
    }
}
