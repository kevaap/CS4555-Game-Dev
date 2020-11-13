using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class combat : MonoBehaviour
{
    // heath stats
    public float maxHealth;
    public float health;

    // UI objects
    // to display enemy health
    public GameObject healthUI;
    public Slider sliderHealth;//

    void Start()
    {
      health = maxHealth;
      sliderHealth.value = CalculateHealth();
    }

    void Update()
    {
      sliderHealth.value = CalculateHealth();

      if(health < maxHealth)
      {
        healthUI.SetActive(true);
      }

      if(health > maxHealth)
      {
        health = maxHealth;
      }

      //   Game Over    //
      // Restar Scene  //

      if(health == 0)
      {
        RestartScene();
      }
    }

    float CalculateHealth()
    {
      return health / maxHealth;
    }

    public void TakeDamage(float amount)
    {
      health -= amount;

      sliderHealth.value = CalculateHealth();

      if(health <= 0)
      {
        health = 0;
        //Debug.Log("With God anything is possible!");
      }
    }

    public void RestartScene()
    {
      Scene thisScene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(thisScene.name);
    }
}
