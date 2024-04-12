using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPointScript : MonoBehaviour
{
    Collider2D collider;
    [SerializeField]
    Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.position = respawnPoint.position;
            }
        }
    }
}
