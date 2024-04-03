using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    // luego arreglo esto perdon
    Vector2 positionToFall;
    float speed;
    Collider2D collider2d;
    int layerSuelo;
    bool colocado;

    [SerializeField] float limLeft;
    [SerializeField] float limRight;
    float radio;
    GameObject player;

    public float Speed { get => speed; set { speed = value; } }
    public Vector2 PositionToFall { get => positionToFall; set { positionToFall = value; } }

    public float limitUpper { get; set; }


    private void Start()
    {
        //direccionX = positionToFall.x < 0? -1 : 1;
        collider2d = GetComponent<Collider2D>();
        layerSuelo = LayerMask.GetMask("Ground");
        Debug.Log(layerSuelo);
    }
    // Update is called once per frame
    void Update()
    {
        /*
         * aqui mueren mis esperanzas de hacer tiros parab�licos
        if (Mathf.Abs(transform.position.x) < Mathf.Abs(positionToFall.x/2))
        {
            transform.position += new Vector3(direccionX * speed * Time.deltaTime, speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(direccionX * speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        }
        */
        if(transform.position.y < limitUpper && !colocado)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else
        {
            if(!colocado)
            {
                SetLandingPoint();
                transform.position = new Vector3(positionToFall.x, transform.position.y, transform.position.z);
                colocado = true;
                Debug.Log("colocando");
            }
            else
            {
                Debug.Log("cayendo");
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
            }
        }
    }

    public void SetLandingLimits(float limiteDisparoIzquierdo, float limiteDisparoDerecho, float radio, GameObject player)
    {
        limLeft = limiteDisparoIzquierdo;
        limRight = limiteDisparoDerecho;
        this.radio = radio;
        this.player = player;

    }

    private void SetLandingPoint()
    {
        float limiteDisparoIzquierdo = player.transform.position.x - radio <= limLeft ? limLeft : player.transform.position.x - radio;
        float limiteDisparoDerecho = player.transform.position.x + radio >= limRight ? limRight : player.transform.position.x;
        PositionToFall = new Vector2(Random.Range(limiteDisparoIzquierdo, limiteDisparoDerecho), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("toco algo");
        Debug.Log(collision.gameObject);
        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }


}