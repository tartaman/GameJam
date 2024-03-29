using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoRecibirRaño : MonoBehaviour
{
    // Vida actual del enemigo
    [SerializeField]
    int health;

    //vida máxima que puede tener el enemigo
    [SerializeField]
    int maximumHealth;

    // Encapsulación de vida por si se necesita, le puse la restricción de que no puede bajar de 0
    // o pasarse de la maxima, pero pueden quitarsela si no
    public int Health {
        get => health;
        set {
            if (health < 0 || health > maximumHealth)
            {
                throw new ArgumentException($"La vida no debe estar entre 0 y {maximumHealth}");
            }
            health = value;
        }
    }

    public void receiveDamage(int damage)
    {
        // Evitar que caiga a numeros negativos
        if(Health - damage < 0)
        {
            Health = 0;
        }
        // Evitar que pase de 100 por si es curar
        else if (Health - damage > maximumHealth)
        {
            Health = maximumHealth;
        }
        //hacer daño
        else
        {
            Health-= damage;
        }
    }

}
