using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotBreak : MonoBehaviour
{
  Animator animator;

void Start()
{
  animator = GetComponent<Animator>();
}


private void OnTriggerEnter(Collider other)
{
      if(other.tag == "reach")
      {

          animator.SetBool("break",true);
      }
    }

}
