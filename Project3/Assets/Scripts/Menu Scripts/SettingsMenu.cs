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

    int resH;
    int resW;
    float mastV;
    float musV;
    float sfxV;
    int qual;
    bool tog;

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
        SaveSystem.Instance.masterVolume = mastV;
        SaveSystem.Instance.musicVolume = musV;
        SaveSystem.Instance.SFXVolume = sfxV;
        SaveSystem.Instance.resolutionHeight = resH;
        SaveSystem.Instance.resolutionWidth = resW;
        SaveSystem.Instance.quality = qual;
        SaveSystem.Instance.isFullscreen = tog;

        SaveSystem.Instance.SaveSettings();

        audioMixer.SetFloat("MasterVolume", SaveSystem.Instance.masterVolume);
        audioMixer.SetFloat("SFXVolume", SaveSystem.Instance.SFXVolume);
        audioMixer.SetFloat("MusicVolume", SaveSystem.Instance.musicVolume);
        QualitySettings.SetQualityLevel(SaveSystem.Instance.quality);
        Screen.fullScreen = SaveSystem.Instance.isFullscreen;
        Screen.SetResolution(SaveSystem.Instance.resolutionWidth, SaveSystem.Instance.resolutionHeight, SaveSystem.Instance.isFullscreen);
    }

    public void RevertSettings()
    {
        audioMixer.SetFloat("MasterVolume", SaveSystem.Instance.masterVolume);
        audioMixer.SetFloat("SFXVolume", SaveSystem.Instance.SFXVolume);
        audioMixer.SetFloat("MusicVolume", SaveSystem.Instance.musicVolume);
        QualitySettings.SetQualityLevel(SaveSystem.Instance.quality);
        Screen.fullScreen = SaveSystem.Instance.isFullscreen;
        Screen.SetResolution(SaveSystem.Instance.resolutionWidth, SaveSystem.Instance.resolutionHeight, SaveSystem.Instance.isFullscreen);
    }

    public void SetMasterVolume(float master)
    {
        mastV = master;
        audioMixer.SetFloat("MasterVolume", master);
    }
    public void SetSFXVolume(float sfx)
    {
        if (sfx <= -60)
            sfx = -80;

        sfxV = sfx;
        audioMixer.SetFloat("SFXVolume", sfx);
    }
    public void SetMusicVolume(float music)
    {
        if (music <= -60)
            music = -80;
        musV = music;
        audioMixer.SetFloat("MusicVolume", music);
    }

    public void SetQuality(int qualityIndex)
    {
        qual = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        tog = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        resH = resolutions[resolutionIndex].height;
        resW = resolutions[resolutionIndex].width;
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
}
