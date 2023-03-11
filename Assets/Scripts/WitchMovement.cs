using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    int direction = -1;
    float speed = 1;
    float approachStep = 0.05f;
    float approachSpeed = 1f;
    Rigidbody rb;
    Transform playerTransform;
    SpriteRenderer spriteRenderer;

    Color32 myColor = new Color32(0, 255, 255, 255);


    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").transform;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = myColor;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speed * direction, 0, 0);
        MoveTowardsPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        direction *= -1;
        spriteRenderer.flipX = true;
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, playerTransform.position, 0.001f);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
    }

}
