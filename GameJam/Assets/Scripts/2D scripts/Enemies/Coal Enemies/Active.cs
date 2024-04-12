using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToActive;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObjectToActive.SetActive(true);
    }
}
