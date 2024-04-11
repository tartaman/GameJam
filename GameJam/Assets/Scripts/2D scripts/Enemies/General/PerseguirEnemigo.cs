using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerseguirEnemigo : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] protected float speed;
    [SerializeField] protected float distanciaDeAtaque;
    [SerializeField] protected float distanciaParaPerseguir;
    protected GameObject player;
    protected bool canFollow = true;
    protected PatrullarEnemigo patrolControler;
    protected bool rotateWhileAttacking;
    Animator animator;

    [Header("Opcionales")]
    [SerializeField] bool stopWhenReachingSomePoint;
    [SerializeField] Vector2 pointLeft;
    [SerializeField] Vector2 pointRight;


    protected virtual void Awake()
    {
        rotateWhileAttacking = true;
        player = GameObject.FindGameObjectWithTag("Player");
        TryGetComponent<PatrullarEnemigo>(out patrolControler);
        TryGetComponent(out animator);
    }

    protected virtual void Start()
    {

    }

    private void Update()
    {
        if(canFollow) {
            perseguir();
        }
        
    }

    public virtual void perseguir()
    {
        float playerDistance = Vector2.Distance(player.transform.position, transform.position);
        
        
        if(playerDistance <= distanciaParaPerseguir && playerDistance > distanciaDeAtaque)
        {
            if (stopWhenReachingSomePoint && (transform.position.x < pointLeft.x || transform.position.x > pointRight.x)) {
                if (animator != null)
                    animator.SetBool("running", false);
                return;
            }
            Vector2 direccion = player.transform.position - transform.position;
            if (animator != null)
                animator.SetBool("running", true);
            // Debug.Log($"Player X{player.transform.position.x}");
            //Debug.Log($"Enemy X{transform.position.x}");
            //Debug.Log("Atacando");
            if (rotateWhileAttacking)
            {
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
            }
           
            direccion.Normalize();
            transform.position += new Vector3(direccion.x * speed * Time.deltaTime,0,0);
            
        }
        else if(playerDistance<= distanciaDeAtaque)
        {
            atacar();
        }
    }

    public virtual void atacar() {
        //Debug.Log("Ataque");
    }
}
