using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float speed = 2f; // The speed of the object's circular motion
    public float radius = .5f; // The radius of the circle
    public float height = 1f; // The height of the object
    private Vector3 center; // The center of the circle
    public GameObject popPF;
    int direction = 1;

    private bool canMove;
    private bool isFlipped;

    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        print("Spider Movement " + gameObject.name);
        center = transform.position; // Get the starting position of the object
        enemyController = gameObject.GetComponent<EnemyController>();
        if (enemyController == null) print("ERROR: cannot find enemy controller");
    }

    // Update is called once per frame
    void Update()
    {
        GetCanMove();
        if (!canMove) return;
        //print("Height = " + height);
        // Calculate the new position of the object using circular motion
        float angle = (direction * Time.time) * speed; // Calculate the angle based on time and speed
        float x = Mathf.Cos(angle) * radius + center.x; // Calculate the x position of the object
        float z = Mathf.Sin(angle) * radius + center.z; // Calculate the z position of the object
        transform.position = new Vector3(x, transform.position.y, z); // Set the position of the object
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Environment"))
        {
            print("Trigger entered with " + other.name + "by object called " + transform.name);
            FlipObject();
        }

        if (other.CompareTag("Player"))
        {
            print("Hit Player");
        }

        if (other.CompareTag("Weapon"))
        {
            print("Hit weapon");
            GameObject pop = Instantiate(popPF, transform.position, Quaternion.identity);
            Destroy(pop, 1f);
            Destroy(gameObject);
            Destroy(other.gameObject);

        }

    }

    private void FlipObject()
    {
        direction *= -1;

        //isFlipped = !isFlipped;

        //if (isFlipped)
        //{
        //    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // flip the object
        //}
        //else
        //{
        //    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // unflip the object
        //}
    }


    public void SetCanMove(bool b) 
    {
        canMove = b;
    }

    private void GetCanMove()
    {
        canMove = enemyController.GetCanMove();
    }
}