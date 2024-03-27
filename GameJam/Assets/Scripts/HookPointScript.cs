using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointScript : MonoBehaviour
{
    //Para que se le asigne el hookpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHookScript>().puntoGancho = gameObject;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
