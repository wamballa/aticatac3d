using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeapon : MonoBehaviour
{
    Rigidbody rb;
    float projectileSpeed = 6f;
    Transform playerT;
    float initialY;

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(playerT.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(gameObject, 3f);
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Environment"))
        {
            //float lastVelocity = rb.velocity.magnitude;
            //Vector3 direction = Vector3.Reflect(lastVelocity.normalized,
            //                            collision.contacts[0].normal);
            //Destroy(gameObject);
        }
    }
}
