using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    public GameObject carriedAxePF;
    public GameObject weaponPF;
    public Transform spawnTransform;
    public GameObject cameraPF;

    float fireInterval = 0.3f;
    float fireTimer = 0;
    bool canFire = true;

    private bool canSeeCarriedAxe = true;


    // Start is called before the first frame update
    void Start()
    {
        //GetWeaponSpawnTransform();
        fireTimer = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponFire();
        ShowWeapon();

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

    private void ShowWeapon()
    {
        if (canFire)
        {
            carriedAxePF.SetActive(true);
        }
        else
        {
            carriedAxePF.SetActive(false);
        }
    }

    void GetWeaponSpawnTransform()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "WeaponSpawn")
            {
                spawnTransform = t;
                break;
            }

        }
        if (spawnTransform == null) print("ERROR: can't find weapon spawn");
    }

    void WeaponFire()
    {
        if (!canFire) return;
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            Instantiate(weaponPF, spawnTransform.position, transform.rotation);
            canFire = false;
        }
    }


}
