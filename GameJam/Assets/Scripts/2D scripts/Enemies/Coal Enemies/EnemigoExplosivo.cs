using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoExplosivo : PerseguirEnemigo
{
    [Header("Eventos Explosion")]
    [SerializeField] GameObject auraExplosiva;

    [SerializeField] float tiempoExplosion;
    [SerializeField] float cooldown;
    bool atacando;
    Animator animator;
    float originalSpeed;
    //float timer;

    protected override void Start()
    {
        originalSpeed = speed;
        auraExplosiva.SetActive(false);
        TryGetComponent(out animator);
    }


    public override void atacar()
    {
        
        //timer += Time.deltaTime;
        if(!atacando)
        {
            animator.SetBool("atacando", true);
            StartCoroutine(Explotar());
        }
        
    }

    public IEnumerator Explotar()
    {
        atacando = true;
        // Ponemos speed en 0 para que no se mueva y activamos la explosion
        speed = 0;
        patrolControler.canPatrol = false;
        
        // Esperamos el tiempo que debe durar la explosion
        yield return new WaitForSeconds(tiempoExplosion);
        //Desactivamos la explosion
        
        animator.SetBool("atacando", false);
        animator.SetBool("running", false);
        //Esperamos un cooldown para que pueda volver a moverse (queda aturdido despues de explotar porque esta chiquito)
        yield return new WaitForSeconds(cooldown);
       
        speed = originalSpeed;
        atacando = false;
        patrolControler.canPatrol = true;


    }

    public void SetExplosion()
    {
        auraExplosiva.SetActive(true);
    }
}
