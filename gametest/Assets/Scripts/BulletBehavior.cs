using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
  public float damage;

  void OnTriggerEnter(Collider other)
  {
    //Debug.Log("hit " + other.name + "!");
    if(other.CompareTag("Player"))
    {
    Pickup(other);

    }
  }

    void Pickup(Collider player)
    {
      combat stats = player.GetComponent<combat>();
      stats.TakeDamage(damage);
      Debug.Log("Hit " + player.name + "!");

      Destroy(gameObject);
    }
 }

 /*
    var hit = collision.gameObject;
    var hitPlayer = hit.GetComponent<Enemy>();
    if(hitPlayer != null)
    {
      var combat = hit.GetComponent<combat>();
      combat.TakeDamage(damage);
      Debug.Log("hit ");

      Destroy(gameObject);
      */

  /*
  private void OnTriggerEnter(Collider other)
  {
    //print("hit " + other.name + "!");
    Debug.Log("hit " + other.name + "!");

    Destroy(gameObject);
  }
  */
