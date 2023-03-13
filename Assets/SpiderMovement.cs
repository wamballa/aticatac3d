using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float speed = 5f; // The speed of the object's circular motion
    public float radius = 2f; // The radius of the circle
    public float height = 0f; // The height of the object
    private Vector3 center; // The center of the circle

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position; // Get the starting position of the object
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        // Calculate the new position of the object using circular motion
        float angle = Time.time * speed; // Calculate the angle based on time and speed
        float x = Mathf.Cos(angle) * radius + center.x; // Calculate the x position of the object
        float z = Mathf.Sin(angle) * radius + center.z; // Calculate the z position of the object
        transform.position = new Vector3(x, height, z); // Set the position of the object
    }

    public void SetCanMove(bool b) 
    {
        canMove = b;
    }
}