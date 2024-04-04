using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVoladorCarbon : PerseguirEnemigo
{
    [Header("Eventos Disparar")]
    [SerializeField] float timeToShoot;
    [SerializeField] float timeBetweenShoot;
    [SerializeField] int numberOfShots;
    [SerializeField] GameObject shoot;
    [SerializeField] float velocidadDisparo;
    [SerializeField] float timeToShootAgain;
    bool atacando;
    Animator animator;

    protected override void Start()
    {
        TryGetComponent(out animator);
    }

    public override void atacar()
    {
        if (player.transform.position.x >= transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(MathF.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (player.transform.position.x < transform.position.x && transform.localScale.x >= 0)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (!atacando)
        {
            animator.SetBool("atacando", true);
            StartCoroutine(Disparar());
        }
    }

    public IEnumerator Disparar()
    {
        atacando = true;
        //canFollow = false;
        animator.SetBool("running", false);
        patrolControler.canPatrol = false;
        float originalSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(timeToShoot);
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject enemigo = Instantiate(shoot, transform.position, Quaternion.identity);
            enemigo.GetComponent<DisparoEnemigo>().Direccion = player.transform.position - transform.position;
            enemigo.GetComponent<DisparoEnemigo>().Velocidad = velocidadDisparo;

            Vector2 direccion = (player.transform.position - transform.position).normalized;
            float angulo = Mathf.Atan2(direccion.y, direccion.x);
            enemigo.transform.rotation = Quaternion.Euler(0, 0, angulo * Mathf.Rad2Deg);

            //enemigo.transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            yield return new WaitForSeconds(timeBetweenShoot);
        }
        
        
        animator.SetBool("atacando", false);
        StartCoroutine(waitToShoot(originalSpeed));
    }

    IEnumerator waitToShoot(float originalSpeed)
    {
        yield return new WaitForSeconds(timeToShootAgain);
        patrolControler.canPatrol = true;
        speed = originalSpeed;
        atacando = false;
        //canFollow = true;
        
    }
}
