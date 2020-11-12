using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
  public Enemy enemyStats;

  public GameObject bulletPrefab;

  public Transform bulletSpawn;

  public float bulletSpeed = 15f;

  public float lifeTime = 3f;

  void Start()
  {
    enemyStats = GetComponent<Enemy>();
  }

  void Update()
  {
      if(enemyStats.currentHealth > 0)
      {
      Fire();
      }
      else if(enemyStats.currentHealth == 0)
      {
        //Debug.Log("Working");
        //Destroy(gameObject);
        Destroy(bulletPrefab);
      }
  }

  private void Fire()
  {
    GameObject bullet = Instantiate(bulletPrefab);

    Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
      bulletSpawn.parent.GetComponent<Collider>());

    bullet.transform.position = bulletSpawn.position;

    Vector3 rotation = bullet.transform.rotation.eulerAngles;

    bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

    bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

    StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
  }

  private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
  {
    yield return new WaitForSeconds(delay);

    Destroy(bullet);
  }
}
