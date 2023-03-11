using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    private Transform spawnTransform;
    public GameObject weaponPF;

    // Start is called before the first frame update
    void Start()
    {
        GetWeaponSpawnTransform();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponFire();
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
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0)))
        {
            Instantiate(weaponPF, spawnTransform.position, Quaternion.identity);
        }
    }
}
