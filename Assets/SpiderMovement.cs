using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float pauseTime = 1f;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        target = transform;
        if (target == null) print("ERROR: no player found"); else StartCoroutine(MoveSpider());
    }

    private IEnumerator MoveSpider()
    {
        while (true)
        {
            // Move towards the target
            float distance = Vector3.Distance(transform.position, target.position);
            float journeyTime = distance / speed;
            float startTime = Time.time;
            while (Time.time < startTime + journeyTime)
            {
                float fraction = (Time.time - startTime) / journeyTime;
                transform.position = Vector3.Lerp(transform.position, target.position, fraction);
                yield return null;
            }

            // Pause at the target position
            yield return new WaitForSeconds(pauseTime);

            // Move back to the starting position
            distance = Vector3.Distance(transform.position, transform.parent.position);
            journeyTime = distance / speed;
            startTime = Time.time;
            while (Time.time < startTime + journeyTime)
            {
                float fraction = (Time.time - startTime) / journeyTime;
                transform.position = Vector3.Lerp(transform.position, transform.parent.position, fraction);
                yield return null;
            }

            // Pause at the starting position
            yield return new WaitForSeconds(pauseTime);
        }
    }
}
