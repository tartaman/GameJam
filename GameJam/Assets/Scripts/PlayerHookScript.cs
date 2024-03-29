using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookScript : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField]
    private KeyCode HookKey;
    [SerializeField]
    private KeyCode ReleaseKey;
    //Como lo tenemos pensado, el gancho estará en el mapa y solo si está dentro del rango del gancho
    //podrá aderirse a él
    [SerializeField]
    public List<GameObject> puntosGancho;
    [Header("PlayerHookParams")]
    [SerializeField]
    //Necesito saber si tiene dentro del rango un gancho
    private bool IsHooking;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        //Obtenemos el linerenderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Siempre vamos a estar checando si hay hookpoints cerca
        CheckHookPoint();
    }
    private void CheckHookPoint()
    {
        //Mientras tenga un punto gancho asignado
        if (puntosGancho.Count > 0)
        {
            //Mientras hayan ganchos en la lista de ganchos hay que escoger el mas cercano
            GameObject puntoGancho = null;
            //Para comparar ganchos
            float lastDistance = float.MaxValue;
            foreach (var gancho in puntosGancho)
            {
                HookPointScript ganchoScript = gancho.GetComponent<HookPointScript>();
                float currDistance = Vector2.Distance(gancho.transform.position, transform.position);
                if (currDistance < lastDistance && ganchoScript.hookable)
                {
                    puntoGancho = gancho;
                    lastDistance = currDistance;
                }
            }
            //Obtenemos el distancejoint
            DistanceJoint2D dJ = GetComponent<DistanceJoint2D>();
            //Si le da a la tecla del gancho
            if (Input.GetKeyDown(HookKey) && puntoGancho.GetComponent<HookPointScript>().hookable) 
            {
                //como le dio a la tecla y tinene un punto gancho cerca entonces activamos el gancho
                dJ.enabled = true;
                //Activamos la linea para que se vea la cuerda
                lineRenderer.enabled = true;
                //Asignamos el punto del gancho al distance joint
                dJ.connectedBody = puntoGancho.GetComponent<Rigidbody2D>();
                //Asignamos la distancia de la "cuerda"
                dJ.distance = Vector2.Distance(puntoGancho.transform.position, transform.position);
                //Activamos el booleano is hooking
                IsHooking = true;
                //el inicio y el fin del line renderer serán, tu posicion y la de el punto del gancho, respectivamente
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, puntoGancho.transform.position);
            }
            //Si esta enlazado ya con un gancho
            if (IsHooking)
            {
                //Actualizamos el linerenderer mientras esté enganchado
                lineRenderer.SetPosition(0, transform.position);
                //Si le da a la tecla de soltar entonces
                if (Input.GetKeyDown(ReleaseKey))
                {
                    dJ.enabled = false;
                    lineRenderer.enabled = false;
                    IsHooking = false;
                    //Dar un "salto" al soltar la cuerda
                    //GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, GetComponent<PlayerMovementScript>().jumpForce, 0);
                }
            }
            //Si ya no se esta columpiando limpiamos la lista de ganchos
            if (!IsHooking)
            {
                foreach (var gancho in puntosGancho)
                {
                    gancho.GetComponent<HookPointScript>().added = false;
                }
            }
        }
        
    }
}
