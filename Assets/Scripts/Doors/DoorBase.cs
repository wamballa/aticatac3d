using System;
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
        HideTeleports(transform);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void HideTeleports(Transform parent)
    {
        foreach(Transform t in parent)
        {
            //print(t.name);
            if (t.CompareTag("Teleport"))
            {
                t.gameObject.SetActive(false);
            }
            HideTeleports(t);
        }
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
