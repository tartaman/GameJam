using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Player Health")]
    [SerializeField] 
    private float maxHealth;
    [SerializeField] 
    private float currentHealth;
    [SerializeField] 
    FloatingHealthbar healthbar;

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
