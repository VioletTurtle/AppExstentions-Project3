using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private Animator anim = null;
    private NavMeshAgent agent;
    public bool dead = false;
    public Slider slider;


    private float health;
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
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    public void DealDamage(float amount)
    {
        if (amount <= 0) { return; }

        health = Mathf.Max(0, health - amount);
        slider.value = health;

        if (health <= 0)
        {
            dead = true;
            anim.SetBool("isDead", true);
            gameObject.tag = "Dead";
            Destroy(gameObject, 5f);
        }
        slider.value = health;
    }
}