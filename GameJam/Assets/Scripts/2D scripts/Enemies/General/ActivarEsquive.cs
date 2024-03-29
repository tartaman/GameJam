using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarEsquive : MonoBehaviour
{
    PatrullarEnemigo enemigo;
    private void Awake()
    {
        enemigo = transform.parent.GetComponent<PatrullarEnemigo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            enemigo.esquivarObstaculo();
    }
}
