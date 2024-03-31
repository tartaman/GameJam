using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Horno : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] float velocidadDash;
    [SerializeField] float velocidadSalto; 
    [SerializeField] float vidaMax;
    [SerializeField] float vidaActual;
    [SerializeField] float timeBetweenAtacks;

    [Header("Tipos de daños")]
    [SerializeField] float contactDamage;
    [SerializeField] float hardFallDamage;
    [SerializeField] float fireDamage;
    [SerializeField] float smokeDamagePerTick;
    [SerializeField] float timeToDoDamageInSmoke;
    [SerializeField] float healingPerFlame;

    [Header("Complementarios")]
    [SerializeField] GameObject[] ventanillas;
    [SerializeField] Image areaHumo;
    [SerializeField] float nivelOpacidad;
    [SerializeField] float timeToFillWithSmoke;
    [SerializeField] bool bothFansActivated;
    [SerializeField] GameObject flame;
    [SerializeField] int numberOfFlames;
    [SerializeField] float flameSpeed;
    [SerializeField] float limLeft;
    [SerializeField] float limRight;
    [SerializeField] float limBottom;
    [SerializeField] GameObject carbonCurador;
    [SerializeField] float tiempoEntreGenerarCarbon;

    GameObject player;
    Rigidbody2D r2D;
    Animator animator;
    float timer;
    bool atacando;
    bool curandose;
    bool humeando;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        r2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        //animator.SetBool("saltando", true);
        Curarse();
    }

    public void Dash()
    {
        Vector2 direccion =  player.transform.position - transform.position;
        direccion = new Vector2(direccion.x, 0);
        r2D.AddForce(direccion, ForceMode2D.Impulse);

    }
    public void MegaJump()
    {

    }
    public void Humear()
    {
        if(!humeando) {
            StartCoroutine(procesoHumear());
            humeando = true;
        }
        
    }

    public IEnumerator procesoHumear()
    {
        timer = 0;
        float actualOpacity = areaHumo.color.a;

        while (areaHumo.color.a < nivelOpacidad)
        {
            float newOpacity = Mathf.Lerp(actualOpacity, nivelOpacidad, timer);
            timer += Time.deltaTime/ timeToFillWithSmoke;
            areaHumo.color = new Color(areaHumo.color.r, areaHumo.color.g, areaHumo.color.b, newOpacity);
            yield return null;
        }
        StartCoroutine(hacerDañoPorTick());

    }

    public IEnumerator hacerDañoPorTick()
    {
        while (!bothFansActivated)
        {
            //Funcion de hacer daño
            yield return new WaitForSeconds(timeToDoDamageInSmoke);
        }
    }

    public void Curarse()
    {
        
        if(!curandose) {
            curandose = true;
            StartCoroutine(GenerarCuradores());
        }
        
    }

    public IEnumerator GenerarCuradores()
    {
        while (curandose)
        {
            Debug.LogError("aaaaaaa");
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                Instantiate(carbonCurador, new Vector2(limLeft, limBottom), Quaternion.identity);
            }
            else
            {
                Instantiate(carbonCurador, new Vector2(limRight, limBottom), Quaternion.identity);
            }
            yield return new WaitForSeconds(tiempoEntreGenerarCarbon);
        }
    }

    public void LanzaLlamas()
    {

    }
    public void LanzaLlamasAereo()
    {
        StartCoroutine(HacerTiros());
    }

    public IEnumerator HacerTiros()
    {
        atacando = true;
        for (int i = 0; i < numberOfFlames; i++)
        {
            GameObject objeto = Instantiate(flame, transform.position, Quaternion.identity);
            objeto.GetComponent<Flame>().Speed = flameSpeed;
            objeto.GetComponent<Flame>().PositionToFall = new Vector2(Random.Range(limLeft, limRight), limBottom);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
