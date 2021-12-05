using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float pHealth = 100f;
    private float pArmor = 1f;
    public string level = "Level1";

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;

    public GameObject DeathScreen;

    [SerializeField] private HealthSystem hs;
    [SerializeField] private PlayerController pc;

    private void Start()
    {
        hs.armor = SaveSystem.Instance.armor;
    }

    private void Update()
    {
        pHealth = hs.Health;
        pArmor = hs.armor;

        healthText.text = pHealth + "%";
        armorText.text = pArmor.ToString();

        if(pHealth <= 0)
        {
            Invoke("ResetLevel", 10f);
        }
        else
        {
            SaveStats();
        }
    }

    private void ResetLevel()
    {
        Debug.Log("RESETING!!!");
        SaveSystem.Instance.armor = 0;
        SaveSystem.Instance.SaveSettings();
        SaveSystem.Instance.LoadNewGame();
        SceneManager.LoadScene(SaveSystem.Instance.level, LoadSceneMode.Single);
    }

    private void SaveStats()
    {
        SaveSystem.Instance.level = level;
        SaveSystem.Instance.armor = pArmor;
    }
}
