using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour
{
    public float maxHealth;
    public float health;

    void Start()
    {
      health = maxHealth;
    }
    public void TakeDamage(float amount)
    {
      health -= amount;
      if(health <= 0)
      {
        health = 0;
        //Debug.Log("With God anything is possible!");
      }
    }
}
