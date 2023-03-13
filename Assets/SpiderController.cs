using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : EnemyController
{
    //public GameObject sparklePF;
    //public GameObject firstChild;
    //private Animator animator;

    //private SphereCollider sphereCollider;

    //SpiderMovement spiderMovement;

    // Start is called before the first frame update
    void Start()
    {
        //spiderMovement = transform.GetComponent<SpiderMovement>();
        //spiderMovement.SetCanMove(false);

        base.Start();

        //sphereCollider = transform.GetComponent<SphereCollider>(); // Get sphere collider
        //if (sphereCollider == null) print("ERROR: no sphere collider found");
        //animator = transform.GetComponent<Animator>(); // Get animator
        //// Disable enemy animation
        //if (animator != null)
        //{
        //    animator.enabled = false;
        //    spiderMovement.SetCanMove(false);
        //}
        //sphereCollider.enabled = false;         // switch off collisions while spawning

        StartCoroutine(ShowEnemy());
    }

    private void Update()
    {
        //CheckCanMove();
    }

    //IEnumerator ShowEnemy()
    //{
    //    yield return new WaitForSeconds(2);
    //    // switch on collisions while spawning
    //    sphereCollider.enabled = true;
    //    sparklePF.SetActive(false);
    //    firstChild.SetActive(true);
    //    animator.enabled = true;
    //    spiderMovement.SetCanMove(true);
    //}
}
