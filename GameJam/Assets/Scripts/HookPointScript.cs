using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointScript : MonoBehaviour
{
    //Saber si se le agrego al la lista
    public bool added;
    public bool hookable;
    //Para que se le asigne el hookpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!added)
            {
                other.GetComponent<PlayerHookScript>().puntosGancho.Add(gameObject);
            }
            added = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
            hookable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //Si ya tiene un gancho en rango no tiene caso quitarselo hasta que salga y no tenga gancho en rango
        hookable = false;

    }
}
