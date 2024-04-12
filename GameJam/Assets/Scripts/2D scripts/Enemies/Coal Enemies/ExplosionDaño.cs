using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDaño : MonoBehaviour
{
    public float explosionDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHealth>().Damage(explosionDamage);
            }
        }
    }
}
