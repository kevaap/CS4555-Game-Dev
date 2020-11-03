using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    // Enemy stats
    public float maxHealth;
    public float currentHealth;

    // UI objects
    // to display enemy healt
    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {

      currentHealth = maxHealth;
      slider.value = CalculateHealth();
    }

    void Update()
    {
      slider.value = CalculateHealth();

      if(currentHealth < maxHealth)
      {
        healthBarUI.SetActive(true);
      }

      if(currentHealth > maxHealth)
      {
        currentHealth = maxHealth;
      }

    }

    float CalculateHealth()
    {
      return currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
      currentHealth -= damage;

      slider.value = CalculateHealth();

      if (currentHealth <= 0)
      {
        Destroy(healthBarUI, 1.0f);
      }

      // Play hurt animation
      animator.SetTrigger("Hurt");

      if(currentHealth <= 0)
      {
        Die();
      }
    }

    public void Die()
    {
      //Die animation
      animator.SetBool("isDead", true);

      // Disable the enemy
      GetComponent<Collider>().enabled = false;
      this.enabled = false;
    }



}
