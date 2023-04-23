using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public GameObject popPF;

    private int direction = 1;
    private float speed = 1;
    private float approachStep = 0.05f;
    private float approachSpeed = 1f;
    private Rigidbody rb;
    private Transform playerTransform;
    private Vector3 startPosition;

    private bool isFlipped = false;
    private bool hasFoundPlayer = false;
    private bool canMove = false;
    private PlayerController playerController;
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
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasFoundPlayer)
        {
            FindPlayer();
        }
        else if (GetCanMove())
        {
            HandleMovement();
        }
    }

    private void FindPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerController = playerTransform?.GetComponent<PlayerController>();
        if (playerTransform != null && playerController != null)
        {
            hasFoundPlayer = true;
        }
        else
        {
            Debug.LogError("ERROR: cannot find Player");
        }
    }

    private void HandleMovement()
    {
        string tag = transform.tag;

        switch (tag)
        {
            case "Dracula":
                HandleDraculaMovement();
                break;
            case "Mummy":
                HandleMummyMovement();
                break;
            default:
                MoveHorizontally();
                break;
        }

        //if (tag == "Dracula")
        //{
        //    HandleDraculaMovement();
        //}
        //else
        //{
        //    MoveHorizontally();
        //}
    }

    private void HandleMummyMovement()
    {
        if (playerController.GetHasPickup("Leaf"))
        {
            MoveTowardsPlayer();
        }
        else
        {
            if (CanSeePlayer())
            {
                // do nothing & guard key
            }
            else
            {
                transform.position = startPosition;
            }
        }
    }

    private void HandleDraculaMovement()
    {
        if (playerController.GetHasPickup("Crucifix"))
        {
            // do nothing
        }
        else
        {
            if (CanSeePlayer())
            {
                MoveTowardsPlayer();
            }
            else
            {
                transform.position = startPosition;
            }
        }
    }

    private bool CanSeePlayer()
    {
        //bool canSeePlayer = false;

        float distance = Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position));

        //print(distance);

        //RaycastHit hit;
        //int layerMask = ~(1 << LayerMask.NameToLayer("Default")) & ~(1 << LayerMask.NameToLayer("ToBeLit")); // Ignore colliders on the default layer

        //Vector3 rayStart = new Vector3(transform.position.x, 0.5f, transform.position.z);

        //if (Physics.Raycast(rayStart, playerTransform.position - transform.position, out hit, Mathf.Infinity, layerMask))
        //{
        //    Debug.DrawRay(transform.position, playerTransform.position - transform.position);
        //    if (hit.collider.CompareTag("Player"))
        //    {
        //        print(">>>>>>>>>>>>>>>>Dracula Can See Player " + hit.collider.tag);
        //        canSeePlayer = true;
        //    }
        //}
        return (distance < 7);
    }

    private void MoveHorizontally()
    {
        rb.velocity = new Vector3(speed * direction, 0, 0);
    }

    public void SetCanMove(bool b)
    {
        canMove = b;
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, playerTransform.position, 0.01f);
        //transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
        transform.position = newPosition;
    }

    public void FlipObject()
    {
        //print("Flip Movement");
        direction *= -1;

        isFlipped = !isFlipped;
    }

    private bool GetCanMove()
    {
        if (enemyController.GetCanMove())
        {
            if (transform.GetComponent<SphereCollider>() == null)
            {
                print("No SphereC in " + transform.name);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
