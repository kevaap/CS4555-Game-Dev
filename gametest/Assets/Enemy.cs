using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
      currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
      currentHealth -= damage;

      // Play hurt animation
      animator.SetTrigger("Hurt");

      if(currentHealth <= 0)
      {
        Die();
      }
    }

    void Die()
    {
      Debug.Log("Enemy died!");

      //Die animation
      animator.SetBool("isDead", true);

      // Disable the enemy
      GetComponent<Collider>().enabled = false;
      this.enabled = false;
    }



}
