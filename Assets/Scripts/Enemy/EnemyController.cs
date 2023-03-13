using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject sparklePF;
    public GameObject firstChild;
    public GameObject popPF;

    protected Animator animator;


    protected SphereCollider sphereCollider;

    public bool canMove = false;
    
    // Sound stuff
    public AudioClip deathSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        if (audioSource == null) print("ERROR: no audio source on enemy");
        canMove = false;
        sphereCollider = transform.GetComponent<SphereCollider>(); // Get sphere collider
        if (sphereCollider == null) print("ERROR: no sphere collider found");
        animator = transform.GetComponent<Animator>(); // Get animator
        // Disable enemy animation
        if (animator != null)
        {
            animator.enabled = false;
        }
        sphereCollider.enabled = false;         // switch off collisions while spawning
    }

    protected void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Weapon"))
        {
            GameObject pop = Instantiate(popPF, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(deathSound);
            Destroy(pop, 1f);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }



    public IEnumerator ShowEnemy()
    {
        yield return new WaitForSeconds(2);
        // switch on collisions while spawning
        sphereCollider.enabled = true;
        sparklePF.SetActive(false);
        firstChild.SetActive(true);
        animator.enabled = true;
        canMove = true;
        //spiderMovement.SetCanMove(true);
    }

    public bool GetCanMove()
    {
        return canMove;
    }
}
