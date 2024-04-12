using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDaño : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] bool destroyOnCollision;
    [SerializeField] float timeToDestroy;
    [SerializeField] bool hasCooldown;
    [SerializeField] float coolDown;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasCooldown )
        {
            if(timer >= coolDown)
            {
                timer = 0;
                HacerDañoPlayerCollider(collision);
            }
        }
        else
        {
            HacerDañoPlayerCollider(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCooldown)
        {
            if (timer >= coolDown)
            {
                timer = 0;
                HacerDañoPlayerTrigger(collision);
            }
        }
        else
        {
            HacerDañoPlayerTrigger(collision);
        }

    }

    void HacerDañoPlayerCollider(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            if (destroyOnCollision)
            {
                Destroy(gameObject, timeToDestroy);
            }
        }
    }

    void HacerDañoPlayerTrigger(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            if (destroyOnCollision)
            {
                Destroy(gameObject, timeToDestroy);
            }
        }
    }



}
