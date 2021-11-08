using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionsDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider masterVSlider;
    public Slider sfxVSlider;
    public Slider musicVSlider;
    public Toggle windowedToggle;

    public void UpdateUI()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if(resolutions[i].width == SaveSystem.Instance.resolutionWidth && resolutions[i].height == SaveSystem.Instance.resolutionHeight)
            {
                currentRes = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentRes;
        resolutionsDropdown.RefreshShownValue();

        qualityDropdown.value = SaveSystem.Instance.quality;
        qualityDropdown.RefreshShownValue();

        masterVSlider.value = SaveSystem.Instance.masterVolume;
        sfxVSlider.value = SaveSystem.Instance.SFXVolume;
        musicVSlider.value = SaveSystem.Instance.musicVolume;
        windowedToggle.isOn = SaveSystem.Instance.isFullscreen;

    }

    public void SaveSettings()
    {
        SaveSystem.Instance.SaveSettings();
        audioMixer.SetFloat("MasterVolume", SaveSystem.Instance.masterVolume);
        audioMixer.SetFloat("SFXVolume", SaveSystem.Instance.SFXVolume);
        audioMixer.SetFloat("MusicVolume", SaveSystem.Instance.musicVolume);
        QualitySettings.SetQualityLevel(SaveSystem.Instance.quality);
        Screen.fullScreen = SaveSystem.Instance.isFullscreen;
        Screen.SetResolution(SaveSystem.Instance.resolutionWidth, SaveSystem.Instance.resolutionHeight, SaveSystem.Instance.isFullscreen);
    }

    public void SetMasterVolume(float master)
    {
        SaveSystem.Instance.masterVolume = master;
        //audioMixer.SetFloat("MasterVolume", master);
    }
    public void SetSFXVolume(float sfx)
    {
        SaveSystem.Instance.SFXVolume = sfx;
        //audioMixer.SetFloat("SFXVolume", sfx);
    }
    public void SetMusicVolume(float music)
    {
        SaveSystem.Instance.musicVolume = music;
        //audioMixer.SetFloat("MusicVolume", music);
    }

    public void SetQuality(int qualityIndex)
    {
        SaveSystem.Instance.quality = qualityIndex;
        //QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        SaveSystem.Instance.isFullscreen = isFullscreen;
        //Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        SaveSystem.Instance.resolutionHeight = resolutions[resolutionIndex].height;
        SaveSystem.Instance.resolutionWidth = resolutions[resolutionIndex].width;
        //Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
}
