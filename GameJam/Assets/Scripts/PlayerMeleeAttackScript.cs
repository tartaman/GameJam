using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackScript : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int meeleDamage = 5;
    public KeyCode AttackKey;
    private float timer;
    public float tiempoEntreAtaques = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(AttackKey) && timer <= tiempoEntreAtaques)
        {
            StartCoroutine(Attack());
        }
        if (timer < tiempoEntreAtaques) 
        {
            timer += Time.deltaTime;
        
        }
        if (timer > tiempoEntreAtaques)
        {
            timer = 0;
        }
    }
    private IEnumerator Attack()
    {
        timer = 0;
        Debug.Log("Attacks");
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
        yield return new WaitForSeconds(tiempoEntreAtaques);
        animator.SetBool("IsAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

