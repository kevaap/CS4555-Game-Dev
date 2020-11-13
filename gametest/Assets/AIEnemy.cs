using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    // enemy_weapon
    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    public float bulletSpeed = 15f;

    //public float lifeTime = 10f;
    public float lifeTime = 2f;


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
      Fire();

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

    void Fire()
    {
      var bullet = (GameObject)Instantiate(bulletPrefab);

      Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
        bulletSpawn.parent.GetComponent<Collider>());

      bullet.transform.position = bulletSpawn.position;

      Vector3 rotation = bullet.transform.rotation.eulerAngles;

      bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

      bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

      Destroy(bullet, lifeTime);
      //StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
    }

    /*
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
      yield return new WaitForSeconds(delay);


      Destroy(bullet);
    }
    */


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

      if(currentHealth == 0)
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
