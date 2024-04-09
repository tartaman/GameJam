using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int meeleDamage = 5;
    public KeyCode AttackKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(AttackKey))
        {
            Attack();
        }
    }
    void Attack()
    {
        //Play attack animation
        //Detect Enemies inside of attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Deal damage to the enemies
        foreach (IDamageable enemy in hitEnemies)
        {
            if (enemy != null)
            {
                //Hacerle daño
                enemy.takeDamage(meeleDamage);
            }
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

