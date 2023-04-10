using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform player;
    bool hasFoundPlayer = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasFoundPlayer)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null) hasFoundPlayer = true; else print("ERROR: cannot find Player");
        }

        //Debug.DrawRay(transform.position, transform.forward * 2, Color.red, 0.5f);

    }
}
