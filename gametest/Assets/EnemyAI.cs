using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  public float lookRadius = 5f;

  public NavMeshAgent enemy;

  public Transform Player;

  //public float speed;

  public float stoppingDistance;

  public float retreatDistance;

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


  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, lookRadius);
  }


  void Start()
  {
    enemy = GetComponent<NavMeshAgent>();

    currentHealth = maxHealth;
    slider.value = CalculateHealth();
  }

  void Update()
  {

    float distance = Vector3.Distance(Player.position, transform.position);

    if(distance <= lookRadius)
    {
      enemy.SetDestination(Player.position);

      if(distance <= enemy.stoppingDistance)
      {
        FaceTarget();
      }
    }
    else if(distance < stoppingDistance && distance > retreatDistance)
    {
      transform.position = this.transform.position;
    }

    //enemy.SetDestination(Player.position);

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


  void FaceTarget()
  {
    Vector3 direction = (Player.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
