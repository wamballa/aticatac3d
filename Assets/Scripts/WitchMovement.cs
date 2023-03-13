using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    public GameObject popPF;

    int direction = 1;
    float speed = 1;
    float approachStep = 0.05f;
    float approachSpeed = 1f;
    Rigidbody rb;
    Transform playerTransform;

    private bool isFlipped = false;

    private bool hasFoundPlayer = false;

    private bool canMove = false;

    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        enemyController = gameObject.GetComponent<EnemyController>();
        if (enemyController == null) print("ERROR: cannot find enemy controller");
    }

    // Update is called once per frame
    void Update()
    {
        GetCanMove();
        if (hasFoundPlayer)
        {
            if (canMove)
            {
                rb.velocity = new Vector3(speed * direction, 0, 0);
                //MoveTowardsPlayer();
            }
        }
        else
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if (playerTransform != null) hasFoundPlayer = true; else print("ERROR: cannot find Player");
        }
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Environment"))
        {
            FlipObject();
        }
    }

    public void SetCanMove( bool b)
    {
        canMove = b;
    }


    void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, playerTransform.position, 0.001f);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
    }

    private void FlipObject()
    {
        direction *= -1;

        isFlipped = !isFlipped;

        if (isFlipped)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // flip the object
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // unflip the object
        }
    }

    private void GetCanMove()
    {
        canMove = enemyController.GetCanMove();
    }

}
