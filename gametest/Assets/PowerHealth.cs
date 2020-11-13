using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerHealth : MonoBehaviour
{
  public float add_health = 100;

  void OnTriggerEnter (Collider other)
  {
    if (other.CompareTag("Player"))
    {
    Pickup(other);
    }
  }


  void Pickup(Collider player)
  {

    combat stats = player.GetComponent<combat>();
    stats.health += add_health;

    //GetComponent<MeshRenderer>().enabled = false;
    //GetComponent<Collider>().enabled = false;

    Destroy(gameObject);
  }
}
