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

    float originalSpeed;
    //float timer;

    protected override void Start()
    {
        originalSpeed = speed;
        auraExplosiva.SetActive(false);
    }


    public override void atacar()
    {
        //timer += Time.deltaTime;
        if(!atacando)
        {
            StartCoroutine(Explotar());
        }
        
    }

    public IEnumerator Explotar()
    {
        atacando = true;
        // Ponemos speed en 0 para que no se mueva y activamos la explosion
        speed = 0;
        auraExplosiva.SetActive (true);
        // Esperamos el tiempo que debe durar la explosion
        yield return new WaitForSeconds(tiempoExplosion);
        //Desactivamos la explosion
        auraExplosiva.SetActive(false);
        //Esperamos un cooldown para que pueda volver a moverse (queda aturdido despues de explotar porque esta chiquito)
        yield return new WaitForSeconds(cooldown);
        speed = originalSpeed;
        atacando = false;
        
        
    }
}
