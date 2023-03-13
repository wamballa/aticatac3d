using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public GameObject sparklePF;
    public GameObject firstChild;
    private Animator animator;

    SpiderMovement spiderMovement;

    // Start is called before the first frame update
    void Start()
    {
        spiderMovement = transform.GetComponent<SpiderMovement>();
        animator = transform.GetComponent<Animator>();
        if (animator != null)
        {
            print("Stop Playback");
            animator.enabled = false;
            spiderMovement.SetCanMove(false);
        }
        StartCoroutine(ShowEnemy());
    }

    IEnumerator ShowEnemy()
    {
        yield return new WaitForSeconds(2);
        sparklePF.SetActive(false);
        firstChild.SetActive(true);
        animator.enabled = true;
        spiderMovement.SetCanMove(true);
    }
}
