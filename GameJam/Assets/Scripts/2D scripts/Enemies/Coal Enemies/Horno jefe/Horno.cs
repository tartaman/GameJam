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

    [Header("Ataque de humo")]
    [SerializeField] GameObject[] ventanillas;
    [SerializeField] Image areaHumo;
    [SerializeField] float nivelOpacidad;
    [SerializeField] float timeToFillWithSmoke;
    [SerializeField] bool bothFansActivated;

    [Header("Ataque de lanza llamas")]
    [SerializeField] GameObject flame;
    [SerializeField] int numberOfFlames;
    [SerializeField] float flameSpeed;
    [SerializeField] float limLeft;
    [SerializeField] float limRight;
    [SerializeField] float limUpper;
    [SerializeField] float radio;

    [Header("Curarse")]
    [SerializeField] float limBottom;
    [SerializeField] GameObject carbonCurador;
    [SerializeField] float tiempoEntreGenerarCarbon;
    [SerializeField] int numeroDeCarbones;
    [SerializeField] float distanciaParaEmbestir;

    GameObject player;
    Rigidbody2D r2D;
    Animator animator;
    float timer;
    public bool atacando;
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
        Controller();
    }

    public void Controller()
    {
        if(!atacando)
        {
            
            int lowerLimit;
            int upperLimit;

            // Solo va a embestir si el jugador está lo suficientemente cerca, si no, pondrá el limite inferior en 2 para que jamas se pueda tomar en cuenta el 1 
            // en la toma de decisiones de que ataque hacer
            lowerLimit = Vector2.Distance(player.transform.position, transform.position) <= distanciaParaEmbestir ? 1 : 2;

            // El jefe solo se va a a curar si el jefe está por debajo de 60% de vida ( o sea, no tiene caso que se cure teniendo bastante vida )
            //si es menor que el 60% pondrá el limite en 6 para tener en cuenta la funcíón de curarse, si no, la pondrá en 5 y ya no podrá tomarlo en cuenta
            // de verdad ¿por qué tiene que ser exclusivo el límite superior?
            upperLimit = vidaActual <= vidaMax * .60 ? 6 : 5;

            // Selector de ataque
            int attack = Random.Range(lowerLimit, upperLimit);

            switch (attack)
            {
                case 1:
                    Debug.Log("dASH");
                    atacando = true;
                    Dash();
                    break;
                case 3:
                    Debug.Log("sALTAR");
                    // Para más información consulten el script Saltar. Lo tuve que hacer con el animator porque hacer saltos parabólicos está medio chistoso
                    animator.SetBool("saltando", true);
                    atacando = true;
                    break;
                case 2:
                    Debug.Log("humo");
                    // En caso de que ya habia humo, llamamos de nuevo a la funcion controller para que decida un nuevo ataque
                    if (!humeando)
                        Humear();
                    else
                    {
                        Controller();
                        Debug.Log("Recursion");
                    }
                        
                    break;
                case 4:
                    Debug.Log("Lanzar llamas");
                    LanzaLlamasAereo();
                    atacando = true;
                    break;
                case 5:
                    Debug.Log("Curacion");
                    Curarse();
                    atacando = true;
                    break;
                default:
                    break;
            }
        }
        
    }

    public IEnumerator WaitToAttack()
    {
        animator.SetBool("iddle", true);
        yield return new WaitForSeconds(timeBetweenAtacks);
        atacando = false;
        animator.SetBool("iddle", false);
    }

    public void Dash()
    {
        Vector2 direccion =  player.transform.position - transform.position;
        direccion = new Vector2(direccion.x, 0);
        r2D.AddForce(direccion, ForceMode2D.Impulse);
        StartCoroutine(WaitToAttack());

    }

    public void Humear()
    {
        StartCoroutine(procesoHumear());
        humeando = true;
        
    }

    public IEnumerator procesoHumear()
    {
        atacando = true;
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
        StartCoroutine(WaitToAttack());

    }

    public IEnumerator degradarHumo()
    {
        Debug.Log("Quitando humo");
        timer = 0;
        float actualOpacity = areaHumo.color.a;

        while (areaHumo.color.a > 0)
        {
            float newOpacity = Mathf.Lerp(actualOpacity, 0, timer);
            timer += Time.deltaTime / timeToFillWithSmoke;
            areaHumo.color = new Color(areaHumo.color.r, areaHumo.color.g, areaHumo.color.b, newOpacity);
            yield return null;
        }
        humeando = false;
    }

    public IEnumerator hacerDañoPorTick()
    {
        while (!bothFansActivated)
        {
            //Funcion de hacer daño
            yield return new WaitForSeconds(timeToDoDamageInSmoke);
        }
        StartCoroutine(degradarHumo());
    }

    public void Curarse()
    {
        StartCoroutine(GenerarCuradores());
    }

    public IEnumerator GenerarCuradores()
    {
        int carbonesGenerados = 0;
        while (curandose && carbonesGenerados < numeroDeCarbones)
        {
            carbonesGenerados += 1;
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
        StartCoroutine(WaitToAttack());
    }

    public void LanzaLlamasAereo()
    {
        StartCoroutine(HacerTiros());
    }

    public IEnumerator HacerTiros()
    {

        for (int i = 0; i < numberOfFlames; i++)
        {
            GameObject objeto = Instantiate(flame, transform.position, Quaternion.identity);
            objeto.GetComponent<Flame>().Speed = flameSpeed;

            //float limiteDisparoIzquierdo = player.transform.position.x - radio <= limLeft ? limLeft : player.transform.position.x - radio;
            //float limiteDisparoDerecho = player.transform.position.x + radio >= limRight ? limRight : player.transform.position.x;

            //objeto.GetComponent<Flame>().PositionToFall = new Vector2(Random.Range(limiteDisparoIzquierdo, limiteDisparoDerecho), limBottom);

            objeto.GetComponent<Flame>().SetLandingLimits(limLeft, limRight, radio, player);
            objeto.GetComponent<Flame>().limitUpper = limUpper;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(WaitToAttack());
    }
}
