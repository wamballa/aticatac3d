using System;
using UnityEngine;

public class WitchMovement : MonoBehaviour
{
    //public GameObject popPF;

    private int direction = 1;
    private float speed = 1;
    private float approachStep = 0.05f;
    private float approachSpeed = 1f;
    private Rigidbody rb;
    private Transform playerTransform;

    private bool isFlipped = false;
    private bool hasFoundPlayer = false;
    private bool canMove = false;

    private Enemy_Controller enemyController;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseComponents();
    }

    private void InitialiseComponents()
    {
        rb = transform.GetComponent<Rigidbody>();
        enemyController = gameObject.GetComponent<Enemy_Controller>();
        if (enemyController == null) print("ERROR: cannot find enemy controller");
        direction = UnityEngine.Random.Range(0, 2) * 2 - 1; // Generates either -1 or 1

        if (direction == -1)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // flip the object
        }

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
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (playerTransform != null) hasFoundPlayer = true; else print("ERROR: cannot find Player");
        }
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Environment"))
        {
            //print(gameObject.name + " hit wall");
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
        print("Flip Movement");
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
