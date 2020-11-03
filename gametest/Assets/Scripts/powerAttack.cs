using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerAttack : MonoBehaviour
{
    public int multiplier = 2;
    public float duration = 10f;

    void OnTriggerEnter (Collider other)
    {
      if (other.CompareTag("Player"))
      {
        StartCoroutine(Pickup(other));
      }
    }

    IEnumerator Pickup(Collider player)
    {
      // spawn a cool effect
      //Instantiate(pickupEffect, transform.position, transform.rotation);

      // Apply effect to the Player
      animationStateController stats = player.GetComponent<animationStateController>();
      stats.attackDamage *= multiplier;

      // Wait x amount of seconds
      GetComponent<MeshRenderer>().enabled = false;
      GetComponent<Collider>().enabled = false;
      yield return new WaitForSeconds(duration);
      stats.attackDamage /= multiplier;
      // Reverse the effect on our Player


      // Remove power up object
      Destroy(gameObject);
    }
}
