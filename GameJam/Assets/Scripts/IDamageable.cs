using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDamageable
{
    public int maxLife { get; set; }
    public int currLife { get; set; }
    public Slider lifeSlider { get; set; }
    public void takeDamage(int damage);
    public void Die();
}
