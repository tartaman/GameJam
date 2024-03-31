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
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("trying to pull");
            Vector2 origen = transform.position;
            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origen,mousePos - origen, range, layermask);

            if (hit.collider != null)
            {
                //Hit something
                Debug.Log("Hit something");
                //The pullable component in the hit gameobject
                Pullable pullable = hit.transform.GetComponent<Pullable>();
                Debug.Log(pullable);
                Debug.Log(hit.collider.gameObject.name);
                if (pullable != null)
                {
                    //Hit something pullable
                    Debug.Log("Hit something pullable");
                    //La condicion es, si es demasiado pesado para moverlo, entonces no lo mueve, si no, si lo mueve segun la fuerza que le quede
                    Vector2.MoveTowards(origen, hit.transform.position, PullForce - pullable.Mass < 0 ? 0 : PullForce - pullable.Mass);
                    //Update the line renderer
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, (Vector3)hit.point);
                }
            }
        }
    }
}
