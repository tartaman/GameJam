using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSuicida : PerseguirEnemigo
{
    [Header("Eventos lanzamiento")]
    
    //[SerializeField] float timeToAttack;
    [SerializeField] float attackSpeed;
    [SerializeField] float runningAnimationSpeed;
    [SerializeField] float timeToGetTired;
    [SerializeField] float timeResting;

    //[SerializeField] float levantamiento;
    //[SerializeField] float levantamientoSpeed;

    Animator animator;
    float timerToRest;
    bool resting;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent<Animator>(out animator);
    }

    public override void atacar()
    {
        timerToRest += Time.deltaTime;
        Debug.Log(timerToRest);
        if(timerToRest < timeToGetTired) {
            Vector2 direccion = player.transform.position - transform.position;
            direccion.Normalize();
            transform.position += new Vector3(direccion.x * attackSpeed * Time.deltaTime, 0, 0);

            if (player.transform.position.x >= transform.position.x && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(MathF.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                //Debug.Log("Derecha");
            }
            else if (player.transform.position.x < transform.position.x && transform.localScale.x >= 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                //Debug.Log("Izquierda");
            }

            if (animator != null)
            {
                animator.speed = runningAnimationSpeed;
            }
        }
        else
        {
            if(!resting)
            {
                
                StartCoroutine(Rest());
            }
        }
        

    }

    IEnumerator Rest()
    {
        Debug.Log("Se canso");
        resting = true;
        float originalSpeed = speed;
        speed = 0;

        animator.speed = 1;

        if (animator != null) {
            animator.SetBool("tired", true);
        }

        patrolControler.canPatrol = false;
        
        yield return new WaitForSeconds(timeResting);

        
        speed = originalSpeed;
        if (animator != null)
        {
            animator.SetBool("tired", false);
        }
        patrolControler.canPatrol = true;
        timerToRest = 0;
        resting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Player"))
        {
            if(animator != null)
            {
                canFollow = false;
                patrolControler.canPatrol = false;

                animator.SetTrigger("suicided");
                
            }
        }
        */
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
