using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeScript : MonoBehaviour, IDamageable
{
    public int maxLife { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int currLife { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Slider lifeSlider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public void Die()
    {
        throw new System.NotImplementedException();
    }
    public void takeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
