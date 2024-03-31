using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pullable : MonoBehaviour
{
    [SerializeField]
    float mass; //La masa para la cual se tomara en cuenta el gancho, a más masa, menos se movera
    public float Mass { get { return mass; }}
}
