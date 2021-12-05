using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private KeyCode keyCode = KeyCode.None;
    [SerializeField] private GameObject objectToToggle = null;
    [SerializeField] private SettingsMenu SM;

    public GameObject ObjectToToggle
    {
        get { return objectToToggle; }
    }

    public void Start()
    {
        SM.UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
            if(objectToToggle.activeSelf == true)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Inventory>().SaveInventory();
        SaveSystem.Instance.SaveSettings();
    }

    public void ExitGame()
    {
        SaveSystem.Instance.music.Stop();
        ResumeGame();
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
