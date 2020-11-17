using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
  public float damage;

  void OnTriggerEnter(Collider other)
  {
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

 
