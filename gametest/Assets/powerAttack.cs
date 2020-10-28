using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerAttack : MonoBehaviour
{
    //public GameObject pickupEffect;

    public int multiplier = 2;

    void OnTriggerEnter (Collider other)
    {
      if (other.CompareTag("Player"))
      {
        Pickup(other);
      }
    }

    void Pickup(Collider player)
    {
      // spawn a cool effect
      //Instantiate(pickupEffect, transform.position, transform.rotation);

      // Apply effect to the Player
      animationStateController stats = player.GetComponent<animationStateController>();
      stats.attackDamage *= multiplier;

      // Remove power up object
      Destroy(gameObject);
    }
}
