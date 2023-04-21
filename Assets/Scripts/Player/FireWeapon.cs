using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject weaponPrefab;
    public float weaponSpawnHeight = 0.5f;
    public GameObject carriedWeapon;

    [Header("Audio")]
    public AudioClip fireClip;

    [Header("Fire Interval")]
    public float fireInterval = 0.1f;

    private Transform weaponSpawnTransform;


    private float fireTimer = 0f;
    private bool canFire = true;


    void Start()
    {
        weaponSpawnTransform = GameObject.Find("WeaponSpawn").transform;
        if (weaponSpawnTransform == null)
        {
            Debug.LogError("Cannot find WeaponSpawn transform!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFireInterval();
        WeaponFire();
        ShowWeapon();
    }

    private void UpdateFireInterval()
    {
        // Fire interval
        if (!canFire)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                canFire = true;
                fireTimer = fireInterval;
            }
        }
    }

    void WeaponFire()
    {
        if (!canFire) return;

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            audioSource.PlayOneShot(fireClip);

            weaponSpawnTransform.position = new Vector3(
                weaponSpawnTransform.position.x,
                weaponSpawnHeight,
                weaponSpawnTransform.position.z);
            Instantiate(weaponPrefab, weaponSpawnTransform.position, transform.rotation);

            canFire = false;
        }
    }

    private void ShowWeapon()
    {
        carriedWeapon.SetActive(canFire);
    }

    //void GetWeaponSpawnTransform()
    //{
    //    foreach (Transform t in transform)
    //    {
    //        if (t.name == "WeaponSpawn")
    //        {
    //            spawnTransform = t;
    //            break;
    //        }

    //    }
    //    if (spawnTransform == null) print("ERROR: can't find weapon spawn");
    //}

}
