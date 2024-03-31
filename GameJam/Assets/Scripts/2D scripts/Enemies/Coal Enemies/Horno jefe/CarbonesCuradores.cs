using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonesCuradores : MonoBehaviour
{
    Transform boss;
    [SerializeField] float speed;

    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 nuevaPosicion = Vector2.MoveTowards(transform.position, boss.position, speed*Time.deltaTime);
        nuevaPosicion.y = transform.position.y;
        transform.position = nuevaPosicion;
    }
}
