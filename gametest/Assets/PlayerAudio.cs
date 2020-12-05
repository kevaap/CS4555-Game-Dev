using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip powerupSound;
    public AudioClip leverSound;

    public AudioSource audioP;

    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot bossSnapshot;

    public AudioClip[] groundSteps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            audioP.PlayOneShot(powerupSound);
        }

        if (other.CompareTag("BossZone"))
        {
            bossSnapshot.TransitionTo(2.0f);
        }

        if (other.CompareTag("Lever"))
        {
            audioP.PlayOneShot(leverSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BossZone"))
        {
            idleSnapshot.TransitionTo(2.0f);
        }
    }

    public void Footsteps () {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        int r = Random.Range(0, 3);
        if (Physics.Raycast(ray, out hit, 1f))
        {
            switch(hit.transform.tag)
            {
                case "Floor":
                    audioP.PlayOneShot(groundSteps[r]);
                    break;
            }
        }
    }
}
