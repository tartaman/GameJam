using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    Vector2 direccion;
    float velocidad;

    public Vector2 Direccion { get=> direccion; set { direccion = value; } }
    public float Velocidad { get=>velocidad; set { velocidad = value; } }

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        direccion.Normalize();
    }

    private void Update()
    {
        transform.position += (Vector3) direccion * Time.deltaTime * velocidad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            col.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            col.isTrigger = true;
        }
    }
}
