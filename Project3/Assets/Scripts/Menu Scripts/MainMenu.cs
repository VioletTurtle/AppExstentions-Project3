using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SettingsMenu SM;
    public void Start()
    {
        SaveSystem.Instance.LoadSettings();
        SM.UpdateUI();
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadNewGame()
    {
        SaveSystem.Instance.music.Play();
        SaveSystem.Instance.LoadNewGame();
        SceneManager.LoadScene(SaveSystem.Instance.level, LoadSceneMode.Single);
    }
    public void LoadSavedGame()
    {
        SaveSystem.Instance.music.Play();
        SaveSystem.Instance.LoadSavedGame();
        SceneManager.LoadScene(SaveSystem.Instance.level, LoadSceneMode.Single);
    }
}
