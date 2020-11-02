using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class animationStateController : MonoBehaviour
{
  // UI
  [SerializeField] Text countdown;
  public float currentEnemy = 6f;

  // icon
  [SerializeField] RawImage customImage;

  public Animator animator;

  public Transform attackPoint;
  public LayerMask enemyLayers;

  public float attackRange = 0.5f;

  // Player stats
  public float attackDamage = 50f;

  void Start()
  {
  countdown.text = currentEnemy.ToString();
  animator = GetComponent<Animator>();


      //SetStats();
  }

  // UI - SYSTEM //
  /*
  void SetStats()
  {
    healthAmaount.text = attackDamage.ToString();
  }
  */
  void Update()
  {
    countdown.text = currentEnemy.ToString() + " robots";
    if(currentEnemy == 1)
    {
      countdown.text = currentEnemy.ToString() + " robot";
    }
    //SetStats();

    if(attackDamage > 50)
    {
      customImage.enabled = true;

    }

    if(attackDamage == 50)
    {
      customImage.enabled = false;
    }

    var keyboard = Keyboard.current;

    bool isWalking = animator.GetBool("isWalking");
    bool isAttacking = animator.GetBool("isAttacking");
    bool isTurningL = animator.GetBool("isTurningL");
    bool isTurningR = animator.GetBool("isTurningR");
    bool isDucking = animator.GetBool("isDucking");


    bool wPressed = keyboard.wKey.isPressed; //walk
    bool aPressed = keyboard.aKey.isPressed; //turn left
    bool sPressed = keyboard.sKey.isPressed; //duck
    bool dPressed = keyboard.dKey.isPressed; //turn right
    bool iPressed = keyboard.iKey.isPressed; //attack

    if(keyboard != null)
    {
      if(!isWalking && wPressed )

      {
        animator.SetBool("isWalking", true);
      }

      if(isWalking && !wPressed)

      {
        animator.SetBool("isWalking", false);
      }

      //  Attack Call  //
      if(!isAttacking && iPressed)
      {
        Attack();
      }

      if(isAttacking && !iPressed)
      {
        animator.SetBool("isAttacking", false);
      }

      if(!isTurningL && aPressed)
      {
        animator.SetBool("isTurningL", true);
      }

      if(isTurningL && !aPressed)
      {
        animator.SetBool("isTurningL", false);
      }

      if(!isTurningR && dPressed)
      {
        animator.SetBool("isTurningR", true);
      }

      if(isTurningR && !dPressed)
      {
        animator.SetBool("isTurningR", false);
      }
      if(!isDucking && sPressed)
      {
        animator.SetBool("isDucking", true);
      }

      if(isDucking && !sPressed)
      {
        animator.SetBool("isDucking", false);
      }


  }

  }
  // ATTACK - SYSTEM //
  void Attack()
  {
    // play an attack animation
    animator.SetBool("isAttacking", true);

    // Detect enemies in range of attack
    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

    // Damage them
    foreach(Collider enemy in hitEnemies)
    {
      enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

      countdown.enabled = true;

      if(enemy.GetComponent<Enemy>().currentHealth == 0)
      {
        currentEnemy--;
        countdown.text = currentEnemy.ToString();
      }


    }
  }

}
