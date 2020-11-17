using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class animationStateController : MonoBehaviour
{
  // Enemy-countdown
  [SerializeField] Text countdown;
  // Total enemy_robots
  public float currentEnemy = 6f;

  // Power-icon
  [SerializeField] RawImage customImage;

  // Jump-icon
  [SerializeField] RawImage iconJump;

  public Animator animator;

  // variables to reference Teddy Clap-Attack
  public Transform attackPoint; // -> Teddy's hand
  public LayerMask enemyLayers;
  public float attackRange = 0.5f;

  // Player stats
  public float attackDamage = 50f;

  // playerMovement  TRANSFER SCRIPT /////
  public CharacterController controller;//
                                        //
  public float speed = 5f;              //
  float speedRotate = 100.0f;           //
  public float gravity = -9.81f;        //
  public float jumpHeight = 3f;         //
                                        //
  public Transform groundCheck;         //
  public float groundDistance = 0.4f;   //
  public LayerMask groundMask;          //
                                        //
  Vector3 velocity;                     //
  bool isGrounded;                      //
  // TRANSFER SCRIPT ENDS ////////////////

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    // playerMovement TRANSFER SCRIPT //////////////////////
    isGrounded = Physics.CheckSphere(groundCheck.position,//
    groundDistance, groundMask);                          //
                                                          //
    if(isGrounded && velocity.y < 0)                      //
    {                                                     //
      velocity.y = -2f;                                   //
    }                                                     //
    float x = Input.GetAxis("Horizontal");                //
    float z = Input.GetAxis("Vertical");                  //
                                                          ///////
    Vector3 move = transform.right * x + transform.forward * z;//
                                                               //
    //controller.Move(move * speed * Time.deltaTime);          //
                                                               //
    if(Input.GetButtonDown("Jump") && isGrounded)              //
    {                                                          //
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);     //
    }                                                          //
                                                               //
    velocity.y += gravity * Time.deltaTime;                    //
                                                               //
    controller.Move(velocity * Time.deltaTime);                //
    // TRANSFER SCRIPT ENDS ////////////////////////////////////

    // Enemy_robots Countdown
    countdown.text = "- Disassemble " + currentEnemy.ToString() + " Robots";
    if(currentEnemy == 1)
    {
      countdown.text = "- Disassemble " + currentEnemy.ToString() + " Robot";
    }
    else if (currentEnemy == 0)
        {
            countdown.text = "OBJECTIVE COMPLETE";
        }

    // if statement to trigger Powerup-Attack icon
    if(attackDamage > 50)
    {
      customImage.enabled = true;
    }
    if(attackDamage == 50)
    {
      customImage.enabled = false;
    }

    // if statement to trigger Powerup-Jump icon
    if(jumpHeight > 3)
    {
      iconJump.enabled = true;
    }
    if(jumpHeight < 3)
    {
      iconJump.enabled = false;
    }

    // Teddy basic movement and animation
    var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed)
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f * Time.deltaTime * speed);
                movement = transform.TransformDirection(movement);
                controller.Move(movement);
            }
            if (keyboard.aKey.isPressed)
            {
                Vector3 rotation = new Vector3(0.0f, -1.0f * Time.deltaTime * speedRotate, 0.0f);
                transform.Rotate(rotation);
            }
            if (keyboard.sKey.isPressed)
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, -1.0f * Time.deltaTime * speed);
                movement = transform.TransformDirection(movement);
                controller.Move(movement);
            }
            if (keyboard.dKey.isPressed)
            {
                Vector3 rotation = new Vector3(0.0f, 1.0f * Time.deltaTime * speedRotate, 0.0f);
                transform.Rotate(rotation);
            }
        }

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
      if(enemy.CompareTag("enemyAI"))
      {
        enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
      }
      if(enemy.CompareTag("enemy"))
      {
        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

        countdown.enabled = true;

        // To subtract enemies form our currentEnemy we make
        // sure their currentHealt hits 0.
        if(enemy.GetComponent<Enemy>().currentHealth == 0)
        {
          currentEnemy--;
          countdown.text = currentEnemy.ToString();
        }
      }



    }

  }

}
