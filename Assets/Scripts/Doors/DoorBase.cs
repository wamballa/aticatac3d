using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip teleportClip;
    public AudioClip doorClip;

    protected void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    protected void PlayTeleportClip()
    {
        audioSource.PlayOneShot(teleportClip);
    }

    protected void PlayDoorSound()
    {
        audioSource.PlayOneShot(doorClip);
    }
}
