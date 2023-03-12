using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour
{

    public GameObject sparklePF;
    public GameObject firstChild;
    private Animator animator;

    WitchMovement witchMovement;

    // Start is called before the first frame update
    void Start()
    {
        witchMovement = transform.GetComponent<WitchMovement>();
        animator = transform.GetComponent<Animator>();
        if (animator != null)
        {
            print("Stop Playback");
            animator.enabled = false;
            witchMovement.SetCanMove(false);
        }
        StartCoroutine(ShowEnemy());
    }

    IEnumerator ShowEnemy()
    {
        yield return new WaitForSeconds(2);
        sparklePF.SetActive(false);
        firstChild.SetActive(true);
        animator.enabled = true;
        witchMovement.SetCanMove(true);
    }


}
