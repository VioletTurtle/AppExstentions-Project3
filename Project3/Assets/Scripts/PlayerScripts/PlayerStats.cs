using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float pHealth = 100f;
    private float pArmor = 1f;
    private int pBulletsShot = 0;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI bulletsFiredText;

    [SerializeField] private HealthSystem hs;
    [SerializeField] private PlayerController pc;

    private void Update()
    {
        pHealth = hs.Health;
        pArmor = hs.armor;
        pBulletsShot = pc.bulletsFired;

        healthText.text = pHealth + "%";
        armorText.text = pArmor.ToString();
        bulletsFiredText.text = pBulletsShot.ToString();
    }
}