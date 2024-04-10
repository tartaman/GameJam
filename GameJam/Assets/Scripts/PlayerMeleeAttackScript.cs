using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackScript : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int meeleDamage = 5;
    public KeyCode AttackKey;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetBool("IsAttacking", true);
        //Detect Enemies inside of attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Deal damage to the enemies
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //Hacerle daño
                enemyHealth.Damage(meeleDamage);
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

