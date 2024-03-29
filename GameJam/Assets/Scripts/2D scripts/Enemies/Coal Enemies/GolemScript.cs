using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : PerseguirEnemigo
{
    [Header("Eventos Piedras")]
    // Todos los objetos piedras
    [SerializeField] GameObject[] Piedras;
    // Puntos en las esquinas de cada piedra para ver si aún está sobre la tierra
    [SerializeField] Vector2[] puntosCheckeoBorde;
    // Tiempo para poder atacar (tiempo que tarda en hacer el golpe)
    [SerializeField] float timeToAttack;
    // Tiempo en el que cada piedra aparecera
    [SerializeField] float timeBetweenCreatingRocks;
    [SerializeField] float timeToDestroyRocks;
    [SerializeField] LayerMask suelo;

    bool atacando;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < Piedras.Length; i++)
        {
            Piedras[i].SetActive(false);
        }
    }

    public override void atacar()
    {
        if(!atacando)
        {
            StartCoroutine(LanzarPiedras());
        }
    }

    public IEnumerator LanzarPiedras()
    {
        atacando = true;
        float timeToMoveAgain = timeBetweenCreatingRocks * (Piedras.Length -1) + timeToDestroyRocks * Piedras.Length;
        float originalSpeed = speed;
        StartCoroutine(EsperarParaMoverse(timeToMoveAgain, originalSpeed));
        speed = 0;
        yield return new WaitForSeconds(timeToAttack);
        for (int i = 0; i < Piedras.Length; i++)
        {
            // punto que depende de la escala real del enemigo
            Vector3 puntoReal = (Vector3)puntosCheckeoBorde[i];
            if(transform.localScale.x < 0)
            {
                puntoReal.x = puntosCheckeoBorde[i].x * -1;
            }

            if (Physics2D.OverlapCircle(Piedras[i].transform.position +  puntoReal, 0.2f, suelo))
            {
                Piedras[i].SetActive(true);
                StartCoroutine(DesaparecerPiedra(Piedras[i]));
            }
                
            yield return new WaitForSeconds(timeBetweenCreatingRocks);
        }
    }

    private IEnumerator EsperarParaMoverse(float tiempo, float originalSpeed)
    {
        yield return new WaitForSeconds(tiempo);
        speed = originalSpeed;
        atacando = false;
        
    } 

    private IEnumerator DesaparecerPiedra(GameObject piedra)
    {
        yield return new WaitForSeconds(timeToDestroyRocks);
        piedra.SetActive(false);
    }
}
