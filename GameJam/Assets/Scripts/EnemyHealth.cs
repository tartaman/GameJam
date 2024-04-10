using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Enemy Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] FloatingHealthbar healthbar;

    [Header("Enemy Animator")]
    [SerializeField] public Animator animator;

    [Header("Enemy Things")]
    [SerializeField] public Rigidbody2D rb;

    public void Damage(float damageAmount)
    {
        //animator.SetTrigger("Hit");
        rb.velocity = Vector3.zero;
        currentHealth -= damageAmount;

        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        rb.velocity = Vector3.zero;
        //animator.SetBool("Death", true);

        Destroy(gameObject,1.5f);
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
