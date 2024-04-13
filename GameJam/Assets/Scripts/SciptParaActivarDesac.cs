using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciptParaActivarDesac : MonoBehaviour
{
    [SerializeField] GameObject objeto;

    public void Active()
    {
        objeto.SetActive(true);    
    }
    public void Desactive()
    {
        objeto.SetActive(false);
    }
}
