using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoMenu : MonoBehaviour {

    [SerializeField]
    private Dropdown quality;
    [SerializeField]
    private Toggle fullscreen;
    [SerializeField]
    private Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Awake()
    {
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

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution tmp = resolutions[index];
        Screen.SetResolution(tmp.width, tmp.height, Screen.fullScreen);
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
