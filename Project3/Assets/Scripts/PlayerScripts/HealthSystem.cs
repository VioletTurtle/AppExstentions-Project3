using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int maxHealth = 100;
    private Animator anim = null;


    private float health;
    public float armor = 1;
    public float Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    private void Start()
    {
        health = maxHealth;
        anim = this.GetComponent<Animator>();
    }

    public void DealDamage(float amount)
    {
        //Debug.Log($"Damage: {amount}");
        if (amount <= 0) { return; }

        health = Mathf.Max(0, health - (amount/armor));

        if (health <= 0)
        {
            anim.SetBool("isDead", true);
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0) { return; }

        health = Mathf.Min(maxHealth, health + amount);
    }
    public void ResetHealth()
    {
        health = 100;
    }
}