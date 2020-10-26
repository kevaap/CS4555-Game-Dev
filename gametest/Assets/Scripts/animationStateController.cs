using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class animationStateController : MonoBehaviour
{
  Animator animator;

  void Start()
  {
      animator = GetComponent<Animator>();
  }

  void Update()
  {

    var keyboard = Keyboard.current;

    bool isWalking = animator.GetBool("isWalking");
    //bool isJumping = animator.GetBool("isJumping");
    bool isAttacking = animator.GetBool("isAttacking");
    bool isTurningL = animator.GetBool("isTurningL");
    bool isTurningR = animator.GetBool("isTurningR");
    bool isDucking = animator.GetBool("isDucking");


    bool wPressed = keyboard.wKey.isPressed; //walk
    bool aPressed = keyboard.aKey.isPressed; //turn left
    bool sPressed = keyboard.sKey.isPressed; //duck
    bool dPressed = keyboard.dKey.isPressed; //turn right
    bool iPressed = keyboard.iKey.isPressed; //attack
    //bool spacePressed = keyboard.spaceKey.isPressed; //jump

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
      /*
      if(!isJumping && spacePressed)
      {
        animator.SetBool("isJumping", true);
      }

      if(isJumping && !spacePressed)
      {
        animator.SetBool("isJumping", false);
      }
      */
      if(!isAttacking && iPressed)
      {
        animator.SetBool("isAttacking", true);
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
}
