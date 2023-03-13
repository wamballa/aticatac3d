using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    private Transform spawnTransform;
    public GameObject weaponPF;

    float fireInterval = 1f;
    float fireTimer = 0;
    bool canFire = true;


    // Start is called before the first frame update
    void Start()
    {
        GetWeaponSpawnTransform();
        fireTimer = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponFire();
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
            Instantiate(weaponPF, spawnTransform.position, Quaternion.identity);
            canFire = false;
        }
    }
}
