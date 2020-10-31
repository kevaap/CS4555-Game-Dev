using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerAttack : MonoBehaviour
{
    //public GameObject pickupEffect;

    public int multiplier = 2;
    public float duration = 15f;

    void OnTriggerEnter (Collider other)
    {
      if (other.CompareTag("Player"))
      {
        StartCoroutine( Pickup(other) );
      }
    }

    IEnumerator Pickup(Collider player)
    {
      // spawn a cool effect
      //Instantiate(pickupEffect, transform.position, transform.rotation);

      // Apply effect to the Player
      animationStateController stats = player.GetComponent<animationStateController>();
      stats.attackDamage *= multiplier;

      GetComponent<MeshRenderer>().enabled = false;
      GetComponent<Collider>().enabled = false;

      // Wait x amount of seconds
      yield return new WaitForSeconds(duration);
      // Reverse the effect on our Player
      stats.attackDamage /= multiplier;

      // Remove power up object
      Destroy(gameObject);
    }
}
