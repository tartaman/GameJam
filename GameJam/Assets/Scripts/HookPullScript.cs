using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPullScript : MonoBehaviour
{
    [SerializeField]
    float range;
    [SerializeField]
    float PullForce;
    LineRenderer lineRenderer;
    [SerializeField]
    int layermask;
    float timer;
    [SerializeField]
    float HookShowDelay;
    [SerializeField]
    //Valor de 0 a 1 para balancear la pullforce
    private float pullForceBalancer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("trying to pull");
        //    Vector2 origen = transform.position;
        //    Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(origen,mousePos - origen, range, layermask);

        //    if (hit.collider != null)
        //    {
        //        //Hit something
        //        Debug.Log("Hit something");
        //        //The pullable component in the hit gameobject
        //        Pullable pullable = hit.transform.GetComponent<Pullable>();
        //        Debug.Log(pullable);
        //        Debug.Log(hit.collider.gameObject.name);
        //        if (pullable != null)
        //        {
        //            //Hit something pullable
        //            Debug.Log("Hit something pullable");
        //            //La condicion es, si es demasiado pesado para moverlo, entonces no lo mueve, si no, si lo mueve segun la fuerza que le quede
        //            Vector2.MoveTowards(origen, hit.transform.position, PullForce - pullable.Mass < 0 ? 0 : PullForce - pullable.Mass);
        //            //Update the line renderer
        //            lineRenderer.enabled = true;
        //            lineRenderer.SetPosition(0, transform.position);
        //            lineRenderer.SetPosition(1, (Vector3)hit.point);
        //        }
        //    }
        //}
        if (Input.GetButtonDown("Fire1") && timer >= HookShowDelay)
        {
            StartCoroutine(shoot());
        }
        if (timer >= HookShowDelay * 0.2f)
        {
            lineRenderer.enabled = false;
        }
    }
    IEnumerator shoot()
    {
        timer = 0f;
        Debug.Log("dispara");
        yield return new WaitForSeconds(0f);

        lineRenderer.enabled = true;

        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 origen = (Vector2)transform.position;

        lineRenderer.SetPosition(0, (Vector3)origen);
        RaycastHit2D hit = Physics2D.Raycast(origen, mousePos - origen, range);
        if (hit.collider != null)
        {
            Debug.Log($"Hit an object named {hit.collider.name}");
            lineRenderer.SetPosition(1, (Vector3)hit.point);
            if (hit.transform.GetComponent<Pullable>() != null)
            {
                Pullable pullable = hit.transform.GetComponent<Pullable>();
                //The ppullable object rigidbody
                Rigidbody2D rb2d = pullable.GetComponent<Rigidbody2D>();
                Debug.Log("Hit something pullable");
                //pullable.gameObject.transform.position = Vector2.MoveTowards(origen, hit.transform.position, PullForce - pullable.Mass < 0 ? 0 : PullForce - pullable.Mass);
                if (PullForce - pullable.Mass > 0)
                {
                    rb2d.AddForce((Vector3)(-(origen + (mousePos - origen).normalized * range)) * (PullForce - pullable.Mass) * (pullForceBalancer));
                }
                if (PullForce - pullable.Mass <= 0)
                {
                    //Añadir la fuerza pero al rigid body de el jugador
                    GetComponent<Rigidbody2D>().AddForce((Vector3)((origen + (mousePos - origen).normalized * range)) * (PullForce) * (pullForceBalancer));
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, (Vector3)(origen + (mousePos - origen).normalized * range));
        }

    }
}
