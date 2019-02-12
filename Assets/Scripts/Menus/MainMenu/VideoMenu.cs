using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoMenu : MonoBehaviour {

    bool ready;
    [SerializeField]
    private Dropdown quality;
    [SerializeField]
    private Toggle fullscreen;
    [SerializeField]
    private Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void OnEnable()
    {
        ready = false;
        InitResolutionDropDown();
        quality.value = QualitySettings.GetQualityLevel();
        fullscreen.isOn = Screen.fullScreen;
    }

    private void InitResolutionDropDown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string tmp = resolutions[i].width + " x " + resolutions[i].height + " " +
                resolutions[i].refreshRate + "Hz";
            options.Add(tmp);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        ready = true;
    }

    public void SetResolution(int index)
    {
        if (ready)
        {
            Resolution tmp = resolutions[index];
            Screen.SetResolution(tmp.width, tmp.height, Screen.fullScreen);
        }
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool val)
    {
        Screen.fullScreen = val;
    }
}
