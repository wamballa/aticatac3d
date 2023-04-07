using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeapon : MonoBehaviour
{
    //public GameObject bulletPrefab;
    private float bulletForce = 6f;
    private Transform spawnPointTransform;

    private void Start()
    {
        AlignForward();
        ShootBullet();
    }

    private void AlignForward()
    {
        spawnPointTransform = GameObject.Find("WeaponSpawn").GetComponent<Transform>();
        if (spawnPointTransform == null) print("ERROR: no spawn point found");
        transform.rotation = spawnPointTransform.rotation;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward*3, Color.red, 1);

    }

    private void ShootBullet()
    {
        // Get the bullet's Rigidbody
        Rigidbody bulletRb = gameObject.GetComponent<Rigidbody>();

        // Apply force to the bullet's Rigidbody in the forward direction
        bulletRb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);

        Destroy(gameObject, 2);
    }

}
