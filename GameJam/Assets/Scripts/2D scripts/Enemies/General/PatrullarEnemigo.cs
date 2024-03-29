using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PatrullarEnemigo : MonoBehaviour
{
    // Como tal son los puntos máximos a los que puede llegar
    [Header ("Puntos de referencia")]
    [SerializeField]
    Vector3 pointLeft;
    [SerializeField]
    Vector3 pointRight;
    [SerializeField] private Vector3 startPoint;

    [Header("Estadísticas")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    /*
    [Header("Referencias")]
    // Vector 2 que se le debe agregar a la posición actual para tener la posición de un collider que detecte obstaculos
    [SerializeField] Vector2 colliderAgregator;
    // Vector 2 que indica el tamaño del collider que detecta obstaculos
    [SerializeField] Vector2 colliderSize;
    // Layermask de lo que debe saltar
    [SerializeField] LayerMask colliderLayerMaskJump;
    */
    [Header("Salto")]
    [SerializeField]LayerMask layerSuelo;
    Rigidbody2D rb;
    Collider2D c2D;

    [Header("Opcionales")]
    [SerializeField] bool stopPatrolingWhenPlayerNear = false;
    [SerializeField] float rangeToStop;
    [SerializeField] GameObject player;

    bool goingLeft = false;
    bool isCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        c2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(stopPatrolingWhenPlayerNear)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if(distance >= rangeToStop)
            {
                Debug.Log("Patrullando");
                patrullar();
            }

        }
        else
        {
            Debug.Log("Patrullando");
            patrullar();
        }
    }

    public void patrullar()
    {
        // Detecta si ya se paso de los limites y si es así, se mueve hacia el lado contrario
        if (transform.position.x <= pointLeft.x)
        {
            goingLeft = false;
            transform.localScale = new Vector3(1,1,1);
        }
        else if (transform.position.x >= pointRight.x)
        {
            goingLeft = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
            

        if (goingLeft)
        {
            transform.position += (Vector3)new Vector2(x: -1 * speed * Time.deltaTime, y: 0);
        }
        else
        {
            transform.position += (Vector3)new Vector2(x: 1 * speed * Time.deltaTime, y: 0);

        }
    }

    public void esquivarObstaculo()
    {
        //bool obstaculoPresente = Physics2D.OverlapCapsule((Vector2)transform.position + colliderAgregator, colliderSize, CapsuleDirection2D.Horizontal, 0, colliderLayerMaskJump);
        isCollider = Physics2D.IsTouchingLayers(c2D, layerSuelo);
        if (isCollider)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("Salto");
        }
            
        
    }




}
