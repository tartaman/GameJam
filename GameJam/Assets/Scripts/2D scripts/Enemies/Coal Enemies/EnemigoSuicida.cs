using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSuicida : PerseguirEnemigo
{
    [Header("Eventos lanzamiento")]
    
    //[SerializeField] float timeToAttack;
    [SerializeField] float attackSpeed;
    //[SerializeField] float levantamiento;
    //[SerializeField] float levantamientoSpeed;
    bool atacando;
    Rigidbody2D r2D;

    float timerToAttack;

    protected override void Awake()
    {
        base.Awake();
        r2D = GetComponent<Rigidbody2D>();
    }

    public override void atacar()
    {
        Vector2 direccion = player.transform.position - transform.position;
        direccion.Normalize();
        //Debug.Log("Atacando");
        transform.position += new Vector3(direccion.x * attackSpeed * Time.deltaTime, 0, 0);

    }

    /*
    public IEnumerator Launch()
    {
        
        canFollow = false;
        atacando = true;
        timerToAttack = 0;
        Vector2 direccionLanzamiento = transform.position;
        Vector2 posicionOriginal = transform.position;
        speed = 0;
        float gravedadOriginal = r2D.gravityScale;
        r2D.gravityScale = 0;
        while( transform.position.y - posicionOriginal.y < levantamiento &&  timerToAttack < timeToAttack)
        {
            timerToAttack += Time.deltaTime;
            transform.position += Vector3.up * levantamientoSpeed * Time.deltaTime;
            direccionLanzamiento = player.transform.position;
            
            yield return new WaitForSeconds(0);
        }
        while( timerToAttack < timeToAttack)
        {
            timerToAttack += Time.deltaTime;
            direccionLanzamiento = player.transform.position;
            Debug.Log("Esperando");
            yield return new WaitForSeconds(0);
           
        }
        Debug.Log(direccionLanzamiento);
        while(transform.position != (Vector3)direccionLanzamiento)
        {
            Debug.Log("Lanzando");
            direccionLanzamiento.Normalize();
            transform.position += (Vector3)direccionLanzamiento * attackSpeed * Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        atacando = false;
        speed = 12;
        
    }
    */
}
