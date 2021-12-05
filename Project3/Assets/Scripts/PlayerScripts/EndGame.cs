using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    public string NextLevel = "Level2";
    public string ThisLevel = "Level1";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Time.timeScale = 1;
            if (NextLevel != "StartMenu")
            {
                Debug.Log("Next Level!!!");
                other.GetComponent<Inventory>().SaveInventory();
                SaveSystem.Instance.level = NextLevel;
                SaveSystem.Instance.SaveSettings();
                SaveSystem.Instance.LoadSavedGame();
                SceneManager.LoadScene(SaveSystem.Instance.level, LoadSceneMode.Single);
            }
            else
            {
                SaveSystem.Instance.music.Stop();
                Debug.Log("Won Game!!!");
                other.GetComponent<Inventory>().SaveInventory();
                SaveSystem.Instance.level = ThisLevel;
                SaveSystem.Instance.SaveSettings();
                SaveSystem.Instance.LoadSavedGame();
                SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);
            }
        }
    }
}
