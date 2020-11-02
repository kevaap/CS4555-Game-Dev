using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerAttack : MonoBehaviour
{
    // icon
    //[SerializeField] Image customImage;
    //public GameObject pickupEffect;

    // countdown
    //[SerializeField] Text countdown;

    //public int currentEnemy = 6;
    //public int remainingEnemy;

    public int multiplier = 2;
    public float duration = 10f;

    void Update()
    {

    }
    void OnTriggerEnter (Collider other)
    {
      if (other.CompareTag("Player"))
      {
        StartCoroutine(Pickup(other));
      }

      /*
      if (other.CompareTag("Player"))
      {
        customImage.enabled = true;
      }
      */
    }

    IEnumerator Pickup(Collider player)
    {

      //currentTime -= 1 * Time.deltaTime;
      //countdown.text = currentTime.ToString("0");
      /*
      if(currentTime <= 0)
      {
        currentTime = 0;
      }
      */


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
