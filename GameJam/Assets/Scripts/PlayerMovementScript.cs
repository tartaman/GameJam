using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovementScript : MonoBehaviour
{
    //Feature flag
    [Header("Feature flags")]
    [SerializeField]
    bool ActiveHeldJump = true;
    [Header("KeyBinds")]
    [SerializeField]
    private KeyCode RunKey; 
    //Parametros como velocidad etc
    [Header("Player params")]
    [SerializeField]
    float velocidad;
    //fuerza del salto
    [SerializeField]
    public int jumpForce;
    //Hice esta para saber si está en el suelo
    bool isGrounded;
    bool isRunning;
    //Cuanto aumenta la velocidad cuando está corriendo
    [SerializeField]
    float runMult;
    float runningVelocity;
    Rigidbody2D rb;
    [Header("Jump related")]
    //Ambos booleanos para manejar el salto
    bool JumpPressed;
    bool JumpReleased;

    //PAra hacer que deje de subri segun un limite
    private bool startTimer = false;
    //El tiempo que puede saltar
    [SerializeField]
    private float JumpTimer;
    //Timer para saber cuanto tiempo lleva "saltado"
    [SerializeField]
    private float timer;
    //La gravedad para poderlo regresar a la normalidad cuando deje de presionar espacio
    [SerializeField]
    private float gravityScale = 1f;
    [Header("Animator")]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //LA gravedad que le hayamos puesto al rb del jugador
        rb.gravityScale = gravityScale;
        timer = JumpTimer;
        runningVelocity = velocidad * runMult;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckJump();
        CheckRun();
    }

    private void CheckRun()
    {
        if (Input.GetKeyDown(RunKey))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(RunKey))
        {
            isRunning = false;
        }
    }

    private void Move()
    {
        //Horizontal movement -> HzM
        float HzM = Input.GetAxis("Horizontal");
        //Nos movemos diferente si esta corriendo o no
        if (HzM != 0 && isRunning)
        {
            transform.Translate(HzM * runningVelocity * Time.deltaTime, 0, 0);
            animator.SetBool("IsRunning", true);
        }
        if (HzM != 0 && !isRunning)
        {
            animator.SetFloat("Horizontal", Math.Abs(HzM));
            animator.SetBool("IsRunning", false);
            transform.Translate(HzM * velocidad * Time.deltaTime, 0, 0);
        }
        if (HzM > 0)
        {
            transform.localScale = new Vector2(1,1);
        }
        if (HzM < 0)
        {
            transform.localScale = new Vector2(-1,1);
        }
        if (HzM == 0)
        {
            animator.SetFloat("Horizontal", HzM);
        }
            
            
    }
    //Saltar, checamos si está en el suelo y le da al boton de saltar
    private void CheckJump()
    {
        if (isGrounded)
            animator.SetBool("IsGrounded", true);
        else
            animator.SetBool("IsGrounded", false);
        //Que tanto salta segun cuanto tiempo apreta el espacio
        if (isGrounded &&  Input.GetButtonDown("Jump") && ActiveHeldJump)
        { 
            JumpPressed = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            JumpReleased = true;
        }
        //Saltar sin mas si no esta activado el salto feature
        if (isGrounded && Input.GetButtonDown("Jump") && !ActiveHeldJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }
        //Manejo del salto feature
        if (JumpPressed)
        {
            StartJump();
        }
        if (JumpReleased) 
        {
            StopJump();
        }
        CheckTimer();
    }
    //Saber cuando solto el boton de saltar y empezar a bajar
    private void StopJump()
    {
        rb.gravityScale = gravityScale;
        JumpReleased = false;
        timer = JumpTimer;
        startTimer = false;
    }
    //Para que sepamos cuando empezó a saltar desde que presiono el boton de saltar
    private void StartJump()
    {
        //para que siga subiendo cuando saltó
        rb.gravityScale = 0;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        JumpPressed = false;
        startTimer = true;
    }
    private void CheckTimer()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                JumpReleased = true;
            }
        }
    }
    //Checar la tag a ver si esta en el piso o en algo en lo que pueda saltar
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
    //Checar si sale de una colision con el piso entonces ya no está en el piso
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
