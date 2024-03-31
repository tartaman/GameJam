using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    Vector2 positionToFall;
    float speed;

    public float Speed { get => speed; set { speed = value; } }
    public Vector2 PositionToFall { get => positionToFall; set { positionToFall = value; } }

    float direccionX;

    private void Start()
    {
        direccionX = positionToFall.x < 0? -1 : 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) < Mathf.Abs(positionToFall.x/2))
        {
            transform.position += new Vector3(direccionX * speed * Time.deltaTime, speed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position += new Vector3(direccionX * speed * Time.deltaTime, -speed * Time.deltaTime, transform.position.z);
        }
    }
}
